using Rotativa;
using Speech_analytics.Clases;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using Rotativa.Options;
using Speech_analytics.Business;
using Speech_analytics.Data;

namespace Speech_analytics.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //CONTROLADOR PARA GENERAR LAS VISTAS Y VALIDAR LA SESSION
        public ActionResult Random()
        {
            try
            {

                if (Session["Estatus"].ToString() == "Activo" && Session["random"].ToString() == "1")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult CalculadoraPlanes()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo" && Session["planCalculator"].ToString() == "1")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        


        public ActionResult PDFRandom()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo" && Session["random"].ToString() == "1")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult PDF()
        {
            try
            {
                if (true)
                {
                    string id = Convert.ToString(Session["idPDF"]);
                    List<PDFALEATORIO> list = new List<PDFALEATORIO>();
                    list = PDFClass.dataPDF(id);
                    //return View(list);
                    return new Rotativa.ViewAsPdf(list)
                    {
                        PageOrientation = Rotativa.Options.Orientation.Landscape,

                      //FileName = "EfegulViewAsPdf.pdf"
                    };
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public ActionResult PDFSURV()
        {
            try
            {
                if (true)
                {
                    string id = Convert.ToString(Session["idPDF"]);
                    List<PDFALEATORIO> list = new List<PDFALEATORIO>();
                    list = PDFClass.dataPDFSURV(id);
                    //return View(list);
                    return new Rotativa.ViewAsPdf(list)
                    {
                        PageOrientation = Rotativa.Options.Orientation.Landscape,

                        //FileName = "EfegulViewAsPdf.pdf"
                    };
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult ConfigurarAleatoriedad()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo" && Session["random"].ToString() == "1")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ActionName("PDFAleatoriedad")]
        public ActionResult PDFAleatoriedad()
        {

            if (Session["Estatus"].ToString() == "Activo")
            {
                ViewBag.Data = Session["infPDF"];

                return new Rotativa.ViewAsPdf(View())
                {
                    PageOrientation = Rotativa.Options.Orientation.Landscape,

                    FileName = "MiActividad.pdf"
                };

                //return new Rotativa.ViewAsPdf(View());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }


        [HttpPost]
        [ActionName("LoginOff")]
        public ActionResult LoginOff()
        {
            Session["UserName"] = null;
            Session["UserPassword"] = null;
            Session["Estatus"] = "Inactiva";
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ActionName("LoandName")]
        public JsonResult LoandName()
        {
            List<SessionModel> oDataSession = new List<SessionModel>();
            oDataSession.Add(new SessionModel
            {
                UserName = Session["UserName"].ToString(),
                Estatus = Session["Estatus"].ToString(),
                LastLogin = Session["LastLogin"].ToString(),
                Nombre = Session["Nombre"].ToString(),

            });
            return Json(oDataSession, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("LoandListCampaign")]
        public JsonResult LoandListCampaign()
        {
            return Json(RandomClass.lsClientes(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("LoandDataUser")]
        public JsonResult LoandDataUser()
        {

            return Json(RandomClass.dataUser(Convert.ToString(Session["UserName"])), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("LoandRamdomCliente")]
        public JsonResult LoandRamdomCliente(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            Session["dataRamdomCallCustoms"] = RandomClass.dataRamdomCallCustomsAlternativa(data, CCMS);
            return Json(Session["dataRamdomCallCustoms"], JsonRequestBehavior.AllowGet);
        }

        [ActionName("LoandRamdomClienteSurveys")]
        public JsonResult LoandRamdomClienteSurveys(string IdCliente)
        {
            Session["dataRamdomSurveys"] = RandomClass.dataRamdomSurveys(IdCliente);
            return Json(Session["dataRamdomSurveys"], JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ActionName("OnChangeLsCliente")]
        public JsonResult OnChangeLsCliente(string IdCliente)
        {
            return Json(RandomClass.lsSubClientesFromCliente(IdCliente), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("SaveNewDataForPDF_DataBasic")]
        public int SaveNewDataForPDF_DataBasic(PDF_AL_USER oPdf_Al_USER)
        {

            if (RandomClass.SaveNewDataForPDF(oPdf_Al_USER, Session["dataRamdomCallCustoms"]) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        [HttpPost]
        [ActionName("sendEmailAleatoriedad")]
        public int sendEmailAleatoriedad(string idPDF)
        {

            List<PDFALEATORIO> listDataRandomCall = new List<PDFALEATORIO>();
                
            listDataRandomCall = EmailClass.dataPDFCall(idPDF);

            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            string emailTO = EmailClass.emailTO(Convert.ToString(Session["UserName"]));

            if (listDataRandomCall.Count != 0)
            {
                string bodyHTML = @"<!doctype html><html ⚡4email data-css-strict><head><meta charset=""utf-8""><style amp4email-boilerplate>body{visibility:hidden}</style><script async src=""https://cdn.ampproject.org/v0.js""></script><style amp-custom>.es-desk-hidden {	display:none;	float:left;	overflow:hidden;	width:0;	max-height:0;	line-height:0;}.es-button-border:hover a.es-button, .es-button-border:hover button.es-button {	background:#56D66B;	border-color:#56D66B;}.es-button-border:hover {	border-color:#42D159 #42D159 #42D159 #42D159;	background:#56D66B;}body {	width:100%;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}table {	border-collapse:collapse;	border-spacing:0px;}table td, body, .es-wrapper {	padding:0;	Margin:0;}.es-content, .es-header, .es-footer {	table-layout:fixed;	width:100%;}p, hr {	Margin:0;}h1, h2, h3, h4, h5 {	Margin:0;	line-height:120%;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}.es-left {	float:left;}.es-right {	float:right;}.es-p5 {	padding:5px;}.es-p5t {	padding-top:5px;}.es-p5b {	padding-bottom:5px;}.es-p5l {	padding-left:5px;}.es-p5r {	padding-right:5px;}.es-p10 {	padding:10px;}.es-p10t {	padding-top:10px;}.es-p10b {	padding-bottom:10px;}.es-p10l {	padding-left:10px;}.es-p10r {	padding-right:10px;}.es-p15 {	padding:15px;}.es-p15t {	padding-top:15px;}.es-p15b {	padding-bottom:15px;}.es-p15l {	padding-left:15px;}.es-p15r {	padding-right:15px;}.es-p20 {	padding:20px;}.es-p20t {	padding-top:20px;}.es-p20b {	padding-bottom:20px;}.es-p20l {	padding-left:20px;}.es-p20r {	padding-right:20px;}.es-p25 {	padding:25px;}.es-p25t {	padding-top:25px;}.es-p25b {	padding-bottom:25px;}.es-p25l {	padding-left:25px;}.es-p25r {	padding-right:25px;}.es-p30 {	padding:30px;}.es-p30t {	padding-top:30px;}.es-p30b {	padding-bottom:30px;}.es-p30l {	padding-left:30px;}.es-p30r {	padding-right:30px;}.es-p35 {	padding:35px;}.es-p35t {	padding-top:35px;}.es-p35b {	padding-bottom:35px;}.es-p35l {	padding-left:35px;}.es-p35r {	padding-right:35px;}.es-p40 {	padding:40px;}.es-p40t {	padding-top:40px;}.es-p40b {	padding-bottom:40px;}.es-p40l {	padding-left:40px;}.es-p40r {	padding-right:40px;}.es-menu td {	border:0;}s {	text-decoration:line-through;}p, ul li, ol li {	font-family:arial, ""helvetica neue"", helvetica, sans-serif;	line-height:150%;}ul li, ol li {	Margin-bottom:15px;	margin-left:0;}a {	text-decoration:underline;}.es-menu td a {	text-decoration:none;	display:block;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}.es-menu amp-img, .es-button amp-img {	vertical-align:middle;}.es-wrapper {	width:100%;	height:100%;}.es-wrapper-color, .es-wrapper {	background-color:#F6F6F6;}.es-header {	background-color:transparent;}.es-header-body {	background-color:#FFFFFF;}.es-header-body p, .es-header-body ul li, .es-header-body ol li {	color:#333333;	font-size:14px;}.es-header-body a {	color:#2CB543;	font-size:14px;}.es-content-body {	background-color:#FFFFFF;}.es-content-body p, .es-content-body ul li, .es-content-body ol li {	color:#333333;	font-size:14px;}.es-content-body a {	color:#2CB543;	font-size:14px;}.es-footer {	background-color:transparent;}.es-footer-body {	background-color:#FFFFFF;}.es-footer-body p, .es-footer-body ul li, .es-footer-body ol li {	color:#333333;	font-size:14px;}.es-footer-body a {	color:#FFFFFF;	font-size:14px;}.es-infoblock, .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li {	line-height:120%;	font-size:12px;	color:#CCCCCC;}.es-infoblock a {	font-size:12px;	color:#CCCCCC;}h1 {	font-size:30px;	font-style:normal;	font-weight:normal;	color:#333333;}h2 {	font-size:24px;	font-style:normal;	font-weight:normal;	color:#333333;}h3 {	font-size:20px;	font-style:normal;	font-weight:normal;	color:#333333;}.es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {	font-size:30px;}.es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {	font-size:24px;}.es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {	font-size:20px;}a.es-button, button.es-button {	border-style:solid;	border-color:#31CB4B;	border-width:10px 20px 10px 20px;	display:inline-block;	background:#31CB4B;	border-radius:30px;	font-size:18px;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;	font-weight:normal;	font-style:normal;	line-height:120%;	color:#FFFFFF;	text-decoration:none;	width:auto;	text-align:center;}.es-button-border {	border-style:solid solid solid solid;	border-color:#2CB543 #2CB543 #2CB543 #2CB543;	background:#2CB543;	border-width:0px 0px 2px 0px;	display:inline-block;	border-radius:30px;	width:auto;}body {	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}@media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150% } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:30px; text-align:left } h2 { font-size:24px; text-align:left } h3 { font-size:20px; text-align:left } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:30px; text-align:left } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:24px; text-align:left } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px; text-align:left } .es-menu td a { font-size:14px } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:14px } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:14px } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:14px } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px } *[class=""gmail-fix""] { display:none } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left } .es-m-txt-r amp-img { float:right } .es-m-txt-c amp-img { margin:0 auto } .es-m-txt-l amp-img { float:left } .es-button-border { display:inline-block } a.es-button, button.es-button { font-size:18px; display:inline-block } .es-adaptive table, .es-left, .es-right { width:100% } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%; max-width:600px } .es-adapt-td { display:block; width:100% } .adapt-img { width:100%; height:auto } td.es-m-p0 { padding:0px } td.es-m-p0r { padding-right:0px } td.es-m-p0l { padding-left:0px } td.es-m-p0t { padding-top:0px } td.es-m-p0b { padding-bottom:0 } td.es-m-p20b { padding-bottom:20px } .es-mobile-hidden, .es-hidden { display:none } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto; overflow:visible; float:none; max-height:inherit; line-height:inherit } tr.es-desk-hidden { display:table-row } table.es-desk-hidden { display:table } td.es-desk-menu-hidden { display:table-cell } .es-menu td { width:1% } table.es-table-not-adapt, .esd-block-html table { width:auto } table.es-social { display:inline-block } table.es-social td { display:inline-block } .es-desk-hidden { display:table-row; width:auto; overflow:visible; max-height:inherit } }</style></head>
                    <body><div class=""es-wrapper-color""> <!--[if gte mso 9]><v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t""> <v:fill type=""tile"" color=""#f6f6f6""></v:fill> </v:background><![endif]--><table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0""><tr><td valign=""top""><table class=""es-header"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr><td align=""center""><table class=""es-header-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""180"" valign=""top""><![endif]--><table class=""es-left"" cellspacing=""0"" cellpadding=""0"" align=""left""><tr><td class=""es-m-p0r es-m-p20b"" width=""180"" valign=""top"" align=""center""><table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation""><tr><td align=""center"" style=""font-size: 0px"">

                                                <a href=""https://ibb.co/nDR79jY"">
                                                    <img src=""cid:Imagen"" width=""500"" height=""100"">
                                                </a>


                                                <a href=""https://ibb.co/nDR79jY"">
                                                    <img width=""100px"" style=""display:block;"" src=""cid:Imagen"" alt=""Logo-Teleperformance-Original"" border=""0"" />
                                                </a>

                    </td>
                    </tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""360"" valign=""top""><![endif]--><table class=""es-right"" cellspacing=""0"" cellpadding=""0"" align=""right""><tr><td class=""es-m-p0r"" width=""360"" valign=""top"" align=""center""><table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation""><tr><td align=""center"" style=""font-size: 0px""><amp-img class=""adapt-img"" src=""~/Multime/teleperformance-group.svg"" alt style=""display: block"" width=""360"" height=""78"" layout=""responsive""></amp-img></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td></tr></table></td>
                    </tr></table><table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr><td align=""center""><table class=""es-content-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""><table width=""100%"" cellspacing=""0"" cellpadding=""0""><tr><td width=""560"" valign=""top"" align=""center""><table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation""><tr><td align=""left"">
                    <p>Esta información fue enviada por medio de correo automático: No debe dar respuesta por ningún motivo, si no puede visualizar el contenido intente ingresar desde un navegador de confianza.</p></td></tr></table></td></tr></table></td></tr></table></td>
                    </tr></table><table class=""es-footer"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr><td align=""center""><table class=""es-footer-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""><table cellpadding=""0"" cellspacing=""0"" width=""100%""><tr><td width=""560"" align=""center"" valign=""top""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""center"" class=""es-p25"" bgcolor=""#cc0000""><p style=""color: #f9f7f7;line-height: 120%"">Text</p></td></tr></table></td></tr></table></td>
                    </tr><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p0r es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td>
                    <td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right""><tr><td width=""125"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td>
                    </tr><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p0r es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td>
                    <td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right""><tr><td width=""125"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td>
                    </tr><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p0r es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td>
                    <td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right""><tr><td width=""125"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td></tr></table></td></tr></table></td></tr></table></div></body></html>


                    ";

                string bodyHTML2 = @"<h6>Referencia:</h6> <img src=""cid:Imagen"" width=""500"" height=""100"">";


                string bodyHTML3 = @"

                        <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
                        <html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" style=""font-family:arial, 'helvetica neue', helvetica, sans-serif"">
                         <head>
                          <meta charset=""UTF-8"">
                          <meta content=""width=device-width, initial-scale=1"" name=""viewport"">
                          <meta name=""x-apple-disable-message-reformatting"">
                          <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                          <meta content=""telephone=no"" name=""format-detection"">
                          <title>New Template</title><!--[if (mso 16)]>
                            <style type=""text/css"">
                            a {text-decoration: none;}
                            </style>
                            <![endif]--><!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--><!--[if gte mso 9]>
                        <xml>
                            <o:OfficeDocumentSettings>
                            <o:AllowPNG></o:AllowPNG>
                            <o:PixelsPerInch>96</o:PixelsPerInch>
                            </o:OfficeDocumentSettings>
                        </xml>
                        <![endif]--><!--[if !mso]><!-- -->
                          <link href=""https://fonts.googleapis.com/css?family=Roboto:400,400i,700,700i"" rel=""stylesheet""><!--<![endif]-->
                          <style type=""text/css"">
                        .rollover div {
	                        font-size:0;
                        }
                        .rollover:hover .rollover-first {
	                        max-height:0px!important;
	                        display:none!important;
                        }
                        .rollover:hover .rollover-second {
	                        max-height:none!important;
	                        display:block!important;
                        }
                        #outlook a {
	                        padding:0;
                        }
                        .es-button {
	                        mso-style-priority:100!important;
	                        text-decoration:none!important;
                        }
                        a[x-apple-data-detectors] {
	                        color:inherit!important;
	                        text-decoration:none!important;
	                        font-size:inherit!important;
	                        font-family:inherit!important;
	                        font-weight:inherit!important;
	                        line-height:inherit!important;
                        }
                        .es-desk-hidden {
	                        display:none;
	                        float:left;
	                        overflow:hidden;
	                        width:0;
	                        max-height:0;
	                        line-height:0;
	                        mso-hide:all;
                        }
                        [data-ogsb] .es-button {
	                        border-width:0!important;
	                        padding:10px 20px 10px 20px!important;
                        }
                        .es-button-border:hover a.es-button, .es-button-border:hover button.es-button {
	                        background:#56d66b!important;
	                        border-color:#56d66b!important;
                        }
                        .es-button-border:hover {
	                        border-color:#42d159 #42d159 #42d159 #42d159!important;
	                        background:#56d66b!important;
                        }
                        @media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150%!important } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:30px!important; text-align:left } h2 { font-size:24px!important; text-align:left } h3 { font-size:20px!important; text-align:left } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:30px!important; text-align:left } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:24px!important; text-align:left } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important; text-align:left } .es-menu td a { font-size:14px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:14px!important } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:14px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:14px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class=""gmail-fix""] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:inline-block!important } a.es-button, button.es-button { font-size:18px!important; display:inline-block!important } .es-adaptive table, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0px!important } .es-m-p0r { padding-right:0px!important } .es-m-p0l { padding-left:0px!important } .es-m-p0t { padding-top:0px!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important } table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important } .h-auto { height:auto!important } }
                        </style>

                         </head>
                         <body style=""width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
                          <div class=""es-wrapper-color"" style=""background-color:#CFE2F3""><!--[if gte mso 9]>
			                        <v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
				                        <v:fill type=""tile"" color=""#cfe2f3""></v:fill>
			                        </v:background>
		                        <![endif]-->
                           <table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#CFE2F3"">
                             <tr>
                              <td valign=""top"" style=""padding:0;Margin:0"">
                               <table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table class=""es-content-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" class=""es-m-txt-c"" bgcolor=""#b0dbfe"" style=""padding:40px;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px"">Mensaje enviado por correo electrónico automático <strong>NO RESPONDER</strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""50%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #fff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0""><!--[if mso]><table style=""width:800px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:294px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p0r es-m-p20b"" valign=""top"" align=""center"" style=""padding:0;Margin:0;width:294px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img class=""adapt-img"" src=""https://ziienz.stripocdn.email/content/guids/CABINET_0656a71faabe91d7b8b4b24dfc59f1d5/images/imagen1.gif"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""119""></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:486px"" valign=""top""><![endif]-->
                                       <table class=""es-right"" cellpadding=""0"" cellspacing=""0"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                         <tr>
                                          <td align=""left"" style=""padding:0;Margin:0;width:486px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img class=""adapt-img"" src=""https://ziienz.stripocdn.email/content/guids/CABINET_0656a71faabe91d7b8b4b24dfc59f1d5/images/logo_teleperformance_original.png"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""281""></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td></tr></table><![endif]--></td>
                                     </tr>
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""50%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #fff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:25px;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:42px;color:#000000;font-size:35px""><strong>Historial Aleatoriedad</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px"">
                                       <table width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td valign=""top"" align=""center"" style=""padding:0;Margin:0;width:760px"">
                                           <table width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px"">Esta información fue enviada por medio de correo automático: No debe dar respuesta por ningún motivo, si no puede visualizar el contenido, intente ingresar desde un navegador de confianza.</p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #ffff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #ffff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#efefef"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#efefef;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0""><!--[if mso]><table style=""width:800px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:390px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p0r es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:390px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>

                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Referencia: </strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>ID CCMS: Evaluador: </strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Evaluador: </strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Rol: </strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Fecha generada: </strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Cliente:</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:390px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                         <tr>
                                          <td align=""center"" style=""padding:0;Margin:0;width:390px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                            ";
                int con = 0;

                foreach (var oItem in listDataRandomCall)
                {
                    con++;
                    if (con == listDataRandomCall.Count())
                    {
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.ID_PDF2 + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.CCMS_EVALUATOR + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.Nombre + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.Rol + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.CRETE_DATETIME + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.Nombre_Campaña + "</p></td></tr>";
                        
                    }
                }

                bodyHTML3 += @"                     
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td></tr></table><![endif]--></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table class=""es-footer"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table class=""es-footer-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:25px;padding-bottom:25px""><!--[if mso]><table style=""width:790px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p0r es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>UCUD</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>DURACIÓN</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>TIMES HELD</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td align=""center"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>AGENTE INICIAL</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>AGENTE FINAL</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>TALK TIME</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:98px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:98px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>HOLDTIME</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:0px""></td><td style=""width:98px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                         <tr>
                                          <td align=""left"" style=""padding:0;Margin:0;width:98px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>ORIGEN COLGADA</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td></tr></table><![endif]--></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>";

                bodyHTML3 += @"

                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0;font-size:0"">
                                               <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:2px solid #999999;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>";


                        int cont = 1;

                        foreach (var oItem in listDataRandomCall)
                                {
            
                              if (cont < listDataRandomCall.Count())
                              {
                                    bodyHTML3 += @"<table class=""es-footer"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                                     <tr>
                                      <td align=""center"" style=""padding:0;Margin:0"">
                                       <table class=""es-footer-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                         <tr>
                                          <td align=""left"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:25px;padding-bottom:25px""><!--[if mso]><table style=""width:790px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:99px"" valign=""top""><![endif]-->";
                                    bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td class=""es-m-p0r es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.UCID;
                                     bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";


                                    bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.DURACION;
                                    bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                                    bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.TIMES_HELD;
                                    bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                                    bodyHTML3 += @"                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""center"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.AGENTE_INICIAL;
                                    bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                                    bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.AGENTE_FINAL;
                                    bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                                    bodyHTML3 += @"                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.TALK_TIME;
                                    bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";


                                    bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:98px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.HOLD_TIME;
                                    bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                                    bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:98px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.ORIGEN_COLGADA;
                                    bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";


                        bodyHTML3 += @"

                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table>";

                              }

                              cont++;
                        }


                bodyHTML3 += @" 

                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:40px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #ffffff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;border-top:2px solid #666666;border-right:2px solid #666666;border-left:2px solid #666666;width:800px;border-bottom:2px solid #666666"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0""><!--[if mso]><table style=""width:796px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:258px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p0r es-m-p20b"" valign=""top"" align=""center"" style=""padding:0;Margin:0;width:258px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img class=""adapt-img"" src=""https://ziienz.stripocdn.email/content/guids/CABINET_0656a71faabe91d7b8b4b24dfc59f1d5/images/imagen1.gif"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""200""></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:518px"" valign=""top""><![endif]-->
                                       <table class=""es-right"" cellpadding=""0"" cellspacing=""0"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                         <tr>
                                          <td align=""left"" style=""padding:0;Margin:0;width:518px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:24px;color:#333333;font-size:16px""><strong>Información Importante</strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""left"" style=""padding:0;Margin:0;padding-top:10px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px"">Enviado a:" + emailTO; 
                bodyHTML3+= @"</p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td></tr></table><![endif]--></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td style=""padding:0;Margin:0"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" class=""es-menu"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr class=""links"">
                                                  <td align=""center"" valign=""top"" width=""33%"" id=""esd-menu-id-0"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;border:0""><a target=""_blank"" href=""https://google.com"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#6FA8DC;font-size:14px"">Enlaces</a></td>
                                                  <td align=""center"" valign=""top"" width=""33%"" id=""esd-menu-id-1"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;border:0""><a target=""_blank"" href=""https://google.co"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#6FA8DC;font-size:14px"">Enlaces</a></td>
                                                  <td align=""center"" valign=""top"" width=""33%"" id=""esd-menu-id-2"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;border:0""><a target=""_blank"" href=""https://google.co"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#6FA8DC;font-size:14px"">Enlaces</a></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:40px;Margin:0;font-size:0"">
                                               <table cellpadding=""0"" cellspacing=""0"" class=""es-table-not-adapt es-social"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:10px""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png"" alt=""Fb"" title=""Facebook"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:10px""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/twitter-logo-black.png"" alt=""Tw"" title=""Twitter"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:10px""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png"" alt=""Ig"" title=""Instagram"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png"" alt=""Yt"" title=""Youtube"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table>
                          </div>
                         </body>
                        </html>";

                Speech_analytics.Models.EMAILINF obclsCorreo = new Speech_analytics.Models.EMAILINF
                {

                    SERVIDOR = "relay.teleperformance.co",
                    USUARIO = "drilldownreports",
                    EMIALFROM = "drilldownreports@teleperformance.com",
                    EMAILTO = emailTO,
                    CONTRASEÑA = "ecnAR&&M0yEC@z@DyELtUc",
                    PUERTO = "25",
                    AUTENTIFICACION = true,
                    SEGURA = true,
                    PRIORIADA = 0,
                    TIPO = 1,
                    ASUNTO = "ALERTA DRILL DOWN REPORTS -- ACTIVIDAD GENERADA PARA CCMS: " + CCMS,
                    IMAGEN = Server.MapPath("~") + @"/Multime/EmailRandom/Ejemplo.gif",
                    IDIMAGEN = "Imagen",
                    MANSAJE = bodyHTML3

                };

                if (EmailClass.setEmail(obclsCorreo) == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }


            }
            else
            {
                return 0;
            }


        }

        [HttpPost]
        [ActionName("sendEmailAleatoriedadSurv")]
        public int sendEmailAleatoriedadSurv(string idPDF)
        {

            List<PDFALEATORIO> listDataRandomCall = new List<PDFALEATORIO>();

            listDataRandomCall = EmailClass.dataPDFSurv(idPDF);

            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            string emailTO = EmailClass.emailTO(Convert.ToString(Session["UserName"]));

            if (listDataRandomCall.Count != 0)
            {
                string bodyHTML = @"<!doctype html><html ⚡4email data-css-strict><head><meta charset=""utf-8""><style amp4email-boilerplate>body{visibility:hidden}</style><script async src=""https://cdn.ampproject.org/v0.js""></script><style amp-custom>.es-desk-hidden {	display:none;	float:left;	overflow:hidden;	width:0;	max-height:0;	line-height:0;}.es-button-border:hover a.es-button, .es-button-border:hover button.es-button {	background:#56D66B;	border-color:#56D66B;}.es-button-border:hover {	border-color:#42D159 #42D159 #42D159 #42D159;	background:#56D66B;}body {	width:100%;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}table {	border-collapse:collapse;	border-spacing:0px;}table td, body, .es-wrapper {	padding:0;	Margin:0;}.es-content, .es-header, .es-footer {	table-layout:fixed;	width:100%;}p, hr {	Margin:0;}h1, h2, h3, h4, h5 {	Margin:0;	line-height:120%;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}.es-left {	float:left;}.es-right {	float:right;}.es-p5 {	padding:5px;}.es-p5t {	padding-top:5px;}.es-p5b {	padding-bottom:5px;}.es-p5l {	padding-left:5px;}.es-p5r {	padding-right:5px;}.es-p10 {	padding:10px;}.es-p10t {	padding-top:10px;}.es-p10b {	padding-bottom:10px;}.es-p10l {	padding-left:10px;}.es-p10r {	padding-right:10px;}.es-p15 {	padding:15px;}.es-p15t {	padding-top:15px;}.es-p15b {	padding-bottom:15px;}.es-p15l {	padding-left:15px;}.es-p15r {	padding-right:15px;}.es-p20 {	padding:20px;}.es-p20t {	padding-top:20px;}.es-p20b {	padding-bottom:20px;}.es-p20l {	padding-left:20px;}.es-p20r {	padding-right:20px;}.es-p25 {	padding:25px;}.es-p25t {	padding-top:25px;}.es-p25b {	padding-bottom:25px;}.es-p25l {	padding-left:25px;}.es-p25r {	padding-right:25px;}.es-p30 {	padding:30px;}.es-p30t {	padding-top:30px;}.es-p30b {	padding-bottom:30px;}.es-p30l {	padding-left:30px;}.es-p30r {	padding-right:30px;}.es-p35 {	padding:35px;}.es-p35t {	padding-top:35px;}.es-p35b {	padding-bottom:35px;}.es-p35l {	padding-left:35px;}.es-p35r {	padding-right:35px;}.es-p40 {	padding:40px;}.es-p40t {	padding-top:40px;}.es-p40b {	padding-bottom:40px;}.es-p40l {	padding-left:40px;}.es-p40r {	padding-right:40px;}.es-menu td {	border:0;}s {	text-decoration:line-through;}p, ul li, ol li {	font-family:arial, ""helvetica neue"", helvetica, sans-serif;	line-height:150%;}ul li, ol li {	Margin-bottom:15px;	margin-left:0;}a {	text-decoration:underline;}.es-menu td a {	text-decoration:none;	display:block;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}.es-menu amp-img, .es-button amp-img {	vertical-align:middle;}.es-wrapper {	width:100%;	height:100%;}.es-wrapper-color, .es-wrapper {	background-color:#F6F6F6;}.es-header {	background-color:transparent;}.es-header-body {	background-color:#FFFFFF;}.es-header-body p, .es-header-body ul li, .es-header-body ol li {	color:#333333;	font-size:14px;}.es-header-body a {	color:#2CB543;	font-size:14px;}.es-content-body {	background-color:#FFFFFF;}.es-content-body p, .es-content-body ul li, .es-content-body ol li {	color:#333333;	font-size:14px;}.es-content-body a {	color:#2CB543;	font-size:14px;}.es-footer {	background-color:transparent;}.es-footer-body {	background-color:#FFFFFF;}.es-footer-body p, .es-footer-body ul li, .es-footer-body ol li {	color:#333333;	font-size:14px;}.es-footer-body a {	color:#FFFFFF;	font-size:14px;}.es-infoblock, .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li {	line-height:120%;	font-size:12px;	color:#CCCCCC;}.es-infoblock a {	font-size:12px;	color:#CCCCCC;}h1 {	font-size:30px;	font-style:normal;	font-weight:normal;	color:#333333;}h2 {	font-size:24px;	font-style:normal;	font-weight:normal;	color:#333333;}h3 {	font-size:20px;	font-style:normal;	font-weight:normal;	color:#333333;}.es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {	font-size:30px;}.es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {	font-size:24px;}.es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {	font-size:20px;}a.es-button, button.es-button {	border-style:solid;	border-color:#31CB4B;	border-width:10px 20px 10px 20px;	display:inline-block;	background:#31CB4B;	border-radius:30px;	font-size:18px;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;	font-weight:normal;	font-style:normal;	line-height:120%;	color:#FFFFFF;	text-decoration:none;	width:auto;	text-align:center;}.es-button-border {	border-style:solid solid solid solid;	border-color:#2CB543 #2CB543 #2CB543 #2CB543;	background:#2CB543;	border-width:0px 0px 2px 0px;	display:inline-block;	border-radius:30px;	width:auto;}body {	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}@media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150% } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:30px; text-align:left } h2 { font-size:24px; text-align:left } h3 { font-size:20px; text-align:left } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:30px; text-align:left } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:24px; text-align:left } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px; text-align:left } .es-menu td a { font-size:14px } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:14px } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:14px } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:14px } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px } *[class=""gmail-fix""] { display:none } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left } .es-m-txt-r amp-img { float:right } .es-m-txt-c amp-img { margin:0 auto } .es-m-txt-l amp-img { float:left } .es-button-border { display:inline-block } a.es-button, button.es-button { font-size:18px; display:inline-block } .es-adaptive table, .es-left, .es-right { width:100% } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%; max-width:600px } .es-adapt-td { display:block; width:100% } .adapt-img { width:100%; height:auto } td.es-m-p0 { padding:0px } td.es-m-p0r { padding-right:0px } td.es-m-p0l { padding-left:0px } td.es-m-p0t { padding-top:0px } td.es-m-p0b { padding-bottom:0 } td.es-m-p20b { padding-bottom:20px } .es-mobile-hidden, .es-hidden { display:none } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto; overflow:visible; float:none; max-height:inherit; line-height:inherit } tr.es-desk-hidden { display:table-row } table.es-desk-hidden { display:table } td.es-desk-menu-hidden { display:table-cell } .es-menu td { width:1% } table.es-table-not-adapt, .esd-block-html table { width:auto } table.es-social { display:inline-block } table.es-social td { display:inline-block } .es-desk-hidden { display:table-row; width:auto; overflow:visible; max-height:inherit } }</style></head>
                    <body><div class=""es-wrapper-color""> <!--[if gte mso 9]><v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t""> <v:fill type=""tile"" color=""#f6f6f6""></v:fill> </v:background><![endif]--><table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0""><tr><td valign=""top""><table class=""es-header"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr><td align=""center""><table class=""es-header-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""180"" valign=""top""><![endif]--><table class=""es-left"" cellspacing=""0"" cellpadding=""0"" align=""left""><tr><td class=""es-m-p0r es-m-p20b"" width=""180"" valign=""top"" align=""center""><table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation""><tr><td align=""center"" style=""font-size: 0px"">

                                                <a href=""https://ibb.co/nDR79jY"">
                                                    <img src=""cid:Imagen"" width=""500"" height=""100"">
                                                </a>


                                                <a href=""https://ibb.co/nDR79jY"">
                                                    <img width=""100px"" style=""display:block;"" src=""cid:Imagen"" alt=""Logo-Teleperformance-Original"" border=""0"" />
                                                </a>

                    </td>
                    </tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""360"" valign=""top""><![endif]--><table class=""es-right"" cellspacing=""0"" cellpadding=""0"" align=""right""><tr><td class=""es-m-p0r"" width=""360"" valign=""top"" align=""center""><table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation""><tr><td align=""center"" style=""font-size: 0px""><amp-img class=""adapt-img"" src=""~/Multime/teleperformance-group.svg"" alt style=""display: block"" width=""360"" height=""78"" layout=""responsive""></amp-img></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td></tr></table></td>
                    </tr></table><table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr><td align=""center""><table class=""es-content-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""><table width=""100%"" cellspacing=""0"" cellpadding=""0""><tr><td width=""560"" valign=""top"" align=""center""><table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation""><tr><td align=""left"">
                    <p>Esta información fue enviada por medio de correo automático: No debe dar respuesta por ningún motivo, si no puede visualizar el contenido intente ingresar desde un navegador de confianza.</p></td></tr></table></td></tr></table></td></tr></table></td>
                    </tr></table><table class=""es-footer"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr><td align=""center""><table class=""es-footer-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""><table cellpadding=""0"" cellspacing=""0"" width=""100%""><tr><td width=""560"" align=""center"" valign=""top""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""center"" class=""es-p25"" bgcolor=""#cc0000""><p style=""color: #f9f7f7;line-height: 120%"">Text</p></td></tr></table></td></tr></table></td>
                    </tr><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p0r es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td>
                    <td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right""><tr><td width=""125"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td>
                    </tr><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p0r es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td>
                    <td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right""><tr><td width=""125"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td>
                    </tr><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p0r es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td>
                    <td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right""><tr><td width=""125"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td></tr></table></td></tr></table></td></tr></table></div></body></html>


                    ";

                string bodyHTML2 = @"<h6>Referencia:</h6> <img src=""cid:Imagen"" width=""500"" height=""100"">";


                string bodyHTML3 = @"

                        <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
                        <html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" style=""font-family:arial, 'helvetica neue', helvetica, sans-serif"">
                         <head>
                          <meta charset=""UTF-8"">
                          <meta content=""width=device-width, initial-scale=1"" name=""viewport"">
                          <meta name=""x-apple-disable-message-reformatting"">
                          <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                          <meta content=""telephone=no"" name=""format-detection"">
                          <title>New Template</title><!--[if (mso 16)]>
                            <style type=""text/css"">
                            a {text-decoration: none;}
                            </style>
                            <![endif]--><!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--><!--[if gte mso 9]>
                        <xml>
                            <o:OfficeDocumentSettings>
                            <o:AllowPNG></o:AllowPNG>
                            <o:PixelsPerInch>96</o:PixelsPerInch>
                            </o:OfficeDocumentSettings>
                        </xml>
                        <![endif]--><!--[if !mso]><!-- -->
                          <link href=""https://fonts.googleapis.com/css?family=Roboto:400,400i,700,700i"" rel=""stylesheet""><!--<![endif]-->
                          <style type=""text/css"">
                        .rollover div {
	                        font-size:0;
                        }
                        .rollover:hover .rollover-first {
	                        max-height:0px!important;
	                        display:none!important;
                        }
                        .rollover:hover .rollover-second {
	                        max-height:none!important;
	                        display:block!important;
                        }
                        #outlook a {
	                        padding:0;
                        }
                        .es-button {
	                        mso-style-priority:100!important;
	                        text-decoration:none!important;
                        }
                        a[x-apple-data-detectors] {
	                        color:inherit!important;
	                        text-decoration:none!important;
	                        font-size:inherit!important;
	                        font-family:inherit!important;
	                        font-weight:inherit!important;
	                        line-height:inherit!important;
                        }
                        .es-desk-hidden {
	                        display:none;
	                        float:left;
	                        overflow:hidden;
	                        width:0;
	                        max-height:0;
	                        line-height:0;
	                        mso-hide:all;
                        }
                        [data-ogsb] .es-button {
	                        border-width:0!important;
	                        padding:10px 20px 10px 20px!important;
                        }
                        .es-button-border:hover a.es-button, .es-button-border:hover button.es-button {
	                        background:#56d66b!important;
	                        border-color:#56d66b!important;
                        }
                        .es-button-border:hover {
	                        border-color:#42d159 #42d159 #42d159 #42d159!important;
	                        background:#56d66b!important;
                        }
                        @media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150%!important } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:30px!important; text-align:left } h2 { font-size:24px!important; text-align:left } h3 { font-size:20px!important; text-align:left } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:30px!important; text-align:left } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:24px!important; text-align:left } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important; text-align:left } .es-menu td a { font-size:14px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:14px!important } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:14px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:14px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class=""gmail-fix""] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:inline-block!important } a.es-button, button.es-button { font-size:18px!important; display:inline-block!important } .es-adaptive table, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0px!important } .es-m-p0r { padding-right:0px!important } .es-m-p0l { padding-left:0px!important } .es-m-p0t { padding-top:0px!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important } table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important } .h-auto { height:auto!important } }
                        </style>

                         </head>
                         <body style=""width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
                          <div class=""es-wrapper-color"" style=""background-color:#CFE2F3""><!--[if gte mso 9]>
			                        <v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
				                        <v:fill type=""tile"" color=""#cfe2f3""></v:fill>
			                        </v:background>
		                        <![endif]-->
                           <table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#CFE2F3"">
                             <tr>
                              <td valign=""top"" style=""padding:0;Margin:0"">
                               <table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table class=""es-content-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" class=""es-m-txt-c"" bgcolor=""#b0dbfe"" style=""padding:40px;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px"">Mensaje enviado por correo electrónico automático <strong>NO RESPONDER</strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""50%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #fff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0""><!--[if mso]><table style=""width:800px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:294px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p0r es-m-p20b"" valign=""top"" align=""center"" style=""padding:0;Margin:0;width:294px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img class=""adapt-img"" src=""https://ziienz.stripocdn.email/content/guids/CABINET_0656a71faabe91d7b8b4b24dfc59f1d5/images/imagen1.gif"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""119""></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:486px"" valign=""top""><![endif]-->
                                       <table class=""es-right"" cellpadding=""0"" cellspacing=""0"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                         <tr>
                                          <td align=""left"" style=""padding:0;Margin:0;width:486px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img class=""adapt-img"" src=""https://ziienz.stripocdn.email/content/guids/CABINET_0656a71faabe91d7b8b4b24dfc59f1d5/images/logo_teleperformance_original.png"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""281""></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td></tr></table><![endif]--></td>
                                     </tr>
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""50%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #fff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:25px;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:42px;color:#000000;font-size:35px""><strong>Historial Aleatoriedad</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px"">
                                       <table width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td valign=""top"" align=""center"" style=""padding:0;Margin:0;width:760px"">
                                           <table width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px"">Esta información fue enviada por medio de correo automático: No debe dar respuesta por ningún motivo, si no puede visualizar el contenido, intente ingresar desde un navegador de confianza.</p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #ffff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #ffff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#efefef"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#efefef;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0""><!--[if mso]><table style=""width:800px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:390px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p0r es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:390px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>

                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Referencia: </strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>ID CCMS: Evaluador: </strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Evaluador: </strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Fecha generada: </strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Cliente:</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:390px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                         <tr>
                                          <td align=""center"" style=""padding:0;Margin:0;width:390px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                            ";
                int con = 0;

                foreach (var oItem in listDataRandomCall)
                {
                    con++;
                    if (con == listDataRandomCall.Count())
                    {
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.ID_PDF2 + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.CCMS_EVALUATOR + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.Nombre + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.Rol + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.CRETE_DATETIME + "</p></td></tr>";
                        bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">" + oItem.Nombre_Campaña + "</p></td></tr>";

                    }
                }

                bodyHTML3 += @"                     
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td></tr></table><![endif]--></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table class=""es-footer"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table class=""es-footer-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:25px;padding-bottom:25px""><!--[if mso]><table style=""width:790px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p0r es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>UCUD</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>ID Encuesta</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Pregunta</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td align=""center"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Respuesta</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Split</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Fecha Fin</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:98px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:98px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Login</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:0px""></td><td style=""width:98px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                         <tr>
                                          <td align=""left"" style=""padding:0;Margin:0;width:98px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>ANI</strong></p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td></tr></table><![endif]--></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>";

                bodyHTML3 += @"

                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0;font-size:0"">
                                               <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:2px solid #999999;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>";


                int cont = 1;

                foreach (var oItem in listDataRandomCall)
                {

                    if (cont < listDataRandomCall.Count())
                    {
                        bodyHTML3 += @"<table class=""es-footer"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                                     <tr>
                                      <td align=""center"" style=""padding:0;Margin:0"">
                                       <table class=""es-footer-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                         <tr>
                                          <td align=""left"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:25px;padding-bottom:25px""><!--[if mso]><table style=""width:790px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:99px"" valign=""top""><![endif]-->";
                        bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td class=""es-m-p0r es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.UCID;
                        bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";


                        bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.ID_Encuesta;
                        bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                        bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.STRPregunta;
                        bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                        bodyHTML3 += @"                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""center"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.Respuesta;
                        bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                        bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.Split;
                        bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                        bodyHTML3 += @"                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.Fecha_Fin;
                        bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";


                        bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:98px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.Login;
                        bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";

                        bodyHTML3 += @"<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                             <tr>
                                              <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:98px"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>" + oItem.ANI;
                        bodyHTML3 += @"</strong></p></td></tr></table></td></tr></table><!--[if mso]></td><td style=""width:0px""> </td><td style=""width:98px"" valign=""top""><![endif]-->";


                        bodyHTML3 += @"

                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table>";

                    }

                    cont++;
                }


                bodyHTML3 += @" 

                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:40px;Margin:0;font-size:0"">
                                               <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td style=""padding:0;Margin:0;border-bottom:0px solid #ffffff;background:unset;height:1px;width:100%;margin:0px""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;border-top:2px solid #666666;border-right:2px solid #666666;border-left:2px solid #666666;width:800px;border-bottom:2px solid #666666"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0""><!--[if mso]><table style=""width:796px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:258px"" valign=""top""><![endif]-->
                                       <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                                         <tr>
                                          <td class=""es-m-p0r es-m-p20b"" valign=""top"" align=""center"" style=""padding:0;Margin:0;width:258px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img class=""adapt-img"" src=""https://ziienz.stripocdn.email/content/guids/CABINET_0656a71faabe91d7b8b4b24dfc59f1d5/images/imagen1.gif"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""200""></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:518px"" valign=""top""><![endif]-->
                                       <table class=""es-right"" cellpadding=""0"" cellspacing=""0"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                                         <tr>
                                          <td align=""left"" style=""padding:0;Margin:0;width:518px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:24px;color:#333333;font-size:16px""><strong>Información Importante</strong></p></td>
                                             </tr>
                                             <tr>
                                              <td align=""left"" style=""padding:0;Margin:0;padding-top:10px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px"">Enviado a:" + emailTO;
                bodyHTML3 += @"</p></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table><!--[if mso]></td></tr></table><![endif]--></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td style=""padding:0;Margin:0"">
                                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" class=""es-menu"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr class=""links"">
                                                  <td align=""center"" valign=""top"" width=""33%"" id=""esd-menu-id-0"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;border:0""><a target=""_blank"" href=""https://google.com"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#6FA8DC;font-size:14px"">Enlaces</a></td>
                                                  <td align=""center"" valign=""top"" width=""33%"" id=""esd-menu-id-1"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;border:0""><a target=""_blank"" href=""https://google.co"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#6FA8DC;font-size:14px"">Enlaces</a></td>
                                                  <td align=""center"" valign=""top"" width=""33%"" id=""esd-menu-id-2"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;border:0""><a target=""_blank"" href=""https://google.co"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#6FA8DC;font-size:14px"">Enlaces</a></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table>
                               <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0"">
                                   <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
                                     <tr>
                                      <td align=""left"" style=""padding:0;Margin:0"">
                                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                         <tr>
                                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                             <tr>
                                              <td align=""center"" style=""padding:40px;Margin:0;font-size:0"">
                                               <table cellpadding=""0"" cellspacing=""0"" class=""es-table-not-adapt es-social"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                                 <tr>
                                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:10px""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png"" alt=""Fb"" title=""Facebook"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:10px""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/twitter-logo-black.png"" alt=""Tw"" title=""Twitter"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:10px""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png"" alt=""Ig"" title=""Instagram"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                                                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png"" alt=""Yt"" title=""Youtube"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                                                 </tr>
                                               </table></td>
                                             </tr>
                                           </table></td>
                                         </tr>
                                       </table></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table>
                          </div>
                         </body>
                        </html>";

                Speech_analytics.Models.EMAILINF obclsCorreo = new Speech_analytics.Models.EMAILINF
                {

                    SERVIDOR = "smtp.office365.com",
                    USUARIO = "jhonnn11livestar@hotmail.com",
                    EMIALFROM = "jhonnn11livestar@hotmail.com",
                    EMAILTO = emailTO,
                    CONTRASEÑA = "Martha2002.",
                    PUERTO = "587",
                    AUTENTIFICACION = true,
                    SEGURA = true,
                    PRIORIADA = 0,
                    TIPO = 1,
                    ASUNTO = "ALERTA DRILL DOWN REPORTS -- ACTIVIDAD GENERADA PARA CCMS: " + CCMS,
                    IMAGEN = Server.MapPath("~") + @"/Multime/EmailRandom/Ejemplo.gif",
                    IDIMAGEN = "Imagen",
                    MANSAJE = bodyHTML3

                };

                if (EmailClass.setEmail(obclsCorreo) == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }


        }


        [HttpPost]
        [ActionName("EmailRandom")]
        public int EmailRandom( List<PDFALEATORIO> dataAleatorio)
        {


            string bodyHTML = @"<!doctype html><html ⚡4email data-css-strict><head><meta charset=""utf-8""><style amp4email-boilerplate>body{visibility:hidden}</style><script async src=""https://cdn.ampproject.org/v0.js""></script><style amp-custom>.es-desk-hidden {	display:none;	float:left;	overflow:hidden;	width:0;	max-height:0;	line-height:0;}.es-button-border:hover a.es-button, .es-button-border:hover button.es-button {	background:#56D66B;	border-color:#56D66B;}.es-button-border:hover {	border-color:#42D159 #42D159 #42D159 #42D159;	background:#56D66B;}body {	width:100%;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}table {	border-collapse:collapse;	border-spacing:0px;}table td, body, .es-wrapper {	padding:0;	Margin:0;}.es-content, .es-header, .es-footer {	table-layout:fixed;	width:100%;}p, hr {	Margin:0;}h1, h2, h3, h4, h5 {	Margin:0;	line-height:120%;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}.es-left {	float:left;}.es-right {	float:right;}.es-p5 {	padding:5px;}.es-p5t {	padding-top:5px;}.es-p5b {	padding-bottom:5px;}.es-p5l {	padding-left:5px;}.es-p5r {	padding-right:5px;}.es-p10 {	padding:10px;}.es-p10t {	padding-top:10px;}.es-p10b {	padding-bottom:10px;}.es-p10l {	padding-left:10px;}.es-p10r {	padding-right:10px;}.es-p15 {	padding:15px;}.es-p15t {	padding-top:15px;}.es-p15b {	padding-bottom:15px;}.es-p15l {	padding-left:15px;}.es-p15r {	padding-right:15px;}.es-p20 {	padding:20px;}.es-p20t {	padding-top:20px;}.es-p20b {	padding-bottom:20px;}.es-p20l {	padding-left:20px;}.es-p20r {	padding-right:20px;}.es-p25 {	padding:25px;}.es-p25t {	padding-top:25px;}.es-p25b {	padding-bottom:25px;}.es-p25l {	padding-left:25px;}.es-p25r {	padding-right:25px;}.es-p30 {	padding:30px;}.es-p30t {	padding-top:30px;}.es-p30b {	padding-bottom:30px;}.es-p30l {	padding-left:30px;}.es-p30r {	padding-right:30px;}.es-p35 {	padding:35px;}.es-p35t {	padding-top:35px;}.es-p35b {	padding-bottom:35px;}.es-p35l {	padding-left:35px;}.es-p35r {	padding-right:35px;}.es-p40 {	padding:40px;}.es-p40t {	padding-top:40px;}.es-p40b {	padding-bottom:40px;}.es-p40l {	padding-left:40px;}.es-p40r {	padding-right:40px;}.es-menu td {	border:0;}s {	text-decoration:line-through;}p, ul li, ol li {	font-family:arial, ""helvetica neue"", helvetica, sans-serif;	line-height:150%;}ul li, ol li {	Margin-bottom:15px;	margin-left:0;}a {	text-decoration:underline;}.es-menu td a {	text-decoration:none;	display:block;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}.es-menu amp-img, .es-button amp-img {	vertical-align:middle;}.es-wrapper {	width:100%;	height:100%;}.es-wrapper-color, .es-wrapper {	background-color:#F6F6F6;}.es-header {	background-color:transparent;}.es-header-body {	background-color:#FFFFFF;}.es-header-body p, .es-header-body ul li, .es-header-body ol li {	color:#333333;	font-size:14px;}.es-header-body a {	color:#2CB543;	font-size:14px;}.es-content-body {	background-color:#FFFFFF;}.es-content-body p, .es-content-body ul li, .es-content-body ol li {	color:#333333;	font-size:14px;}.es-content-body a {	color:#2CB543;	font-size:14px;}.es-footer {	background-color:transparent;}.es-footer-body {	background-color:#FFFFFF;}.es-footer-body p, .es-footer-body ul li, .es-footer-body ol li {	color:#333333;	font-size:14px;}.es-footer-body a {	color:#FFFFFF;	font-size:14px;}.es-infoblock, .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li {	line-height:120%;	font-size:12px;	color:#CCCCCC;}.es-infoblock a {	font-size:12px;	color:#CCCCCC;}h1 {	font-size:30px;	font-style:normal;	font-weight:normal;	color:#333333;}h2 {	font-size:24px;	font-style:normal;	font-weight:normal;	color:#333333;}h3 {	font-size:20px;	font-style:normal;	font-weight:normal;	color:#333333;}.es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {	font-size:30px;}.es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {	font-size:24px;}.es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {	font-size:20px;}a.es-button, button.es-button {	border-style:solid;	border-color:#31CB4B;	border-width:10px 20px 10px 20px;	display:inline-block;	background:#31CB4B;	border-radius:30px;	font-size:18px;	font-family:arial, ""helvetica neue"", helvetica, sans-serif;	font-weight:normal;	font-style:normal;	line-height:120%;	color:#FFFFFF;	text-decoration:none;	width:auto;	text-align:center;}.es-button-border {	border-style:solid solid solid solid;	border-color:#2CB543 #2CB543 #2CB543 #2CB543;	background:#2CB543;	border-width:0px 0px 2px 0px;	display:inline-block;	border-radius:30px;	width:auto;}body {	font-family:arial, ""helvetica neue"", helvetica, sans-serif;}@media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150% } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:30px; text-align:left } h2 { font-size:24px; text-align:left } h3 { font-size:20px; text-align:left } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:30px; text-align:left } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:24px; text-align:left } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px; text-align:left } .es-menu td a { font-size:14px } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:14px } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:14px } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:14px } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px } *[class=""gmail-fix""] { display:none } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left } .es-m-txt-r amp-img { float:right } .es-m-txt-c amp-img { margin:0 auto } .es-m-txt-l amp-img { float:left } .es-button-border { display:inline-block } a.es-button, button.es-button { font-size:18px; display:inline-block } .es-adaptive table, .es-left, .es-right { width:100% } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%; max-width:600px } .es-adapt-td { display:block; width:100% } .adapt-img { width:100%; height:auto } td.es-m-p0 { padding:0px } td.es-m-p0r { padding-right:0px } td.es-m-p0l { padding-left:0px } td.es-m-p0t { padding-top:0px } td.es-m-p0b { padding-bottom:0 } td.es-m-p20b { padding-bottom:20px } .es-mobile-hidden, .es-hidden { display:none } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto; overflow:visible; float:none; max-height:inherit; line-height:inherit } tr.es-desk-hidden { display:table-row } table.es-desk-hidden { display:table } td.es-desk-menu-hidden { display:table-cell } .es-menu td { width:1% } table.es-table-not-adapt, .esd-block-html table { width:auto } table.es-social { display:inline-block } table.es-social td { display:inline-block } .es-desk-hidden { display:table-row; width:auto; overflow:visible; max-height:inherit } }</style></head>
                    <body><div class=""es-wrapper-color""> <!--[if gte mso 9]><v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t""> <v:fill type=""tile"" color=""#f6f6f6""></v:fill> </v:background><![endif]--><table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0""><tr><td valign=""top""><table class=""es-header"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr><td align=""center""><table class=""es-header-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""180"" valign=""top""><![endif]--><table class=""es-left"" cellspacing=""0"" cellpadding=""0"" align=""left""><tr><td class=""es-m-p0r es-m-p20b"" width=""180"" valign=""top"" align=""center""><table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation""><tr><td align=""center"" style=""font-size: 0px"">

                                                <a href=""https://ibb.co/nDR79jY"">
                                                    <img src=""cid:Imagen"" width=""500"" height=""100"">
                                                </a>


                                                <a href=""https://ibb.co/nDR79jY"">
                                                    <img width=""100px"" style=""display:block;"" src=""cid:Imagen"" alt=""Logo-Teleperformance-Original"" border=""0"" />
                                                </a>

                    </td>
                    </tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""360"" valign=""top""><![endif]--><table class=""es-right"" cellspacing=""0"" cellpadding=""0"" align=""right""><tr><td class=""es-m-p0r"" width=""360"" valign=""top"" align=""center""><table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation""><tr><td align=""center"" style=""font-size: 0px""><amp-img class=""adapt-img"" src=""~/Multime/teleperformance-group.svg"" alt style=""display: block"" width=""360"" height=""78"" layout=""responsive""></amp-img></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td></tr></table></td>
                    </tr></table><table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr><td align=""center""><table class=""es-content-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""><table width=""100%"" cellspacing=""0"" cellpadding=""0""><tr><td width=""560"" valign=""top"" align=""center""><table width=""100%"" cellspacing=""0"" cellpadding=""0"" role=""presentation""><tr><td align=""left"">
                    <p>Esta información fue enviada por medio de correo automático: No debe dar respuesta por ningún motivo, si no puede visualizar el contenido intente ingresar desde un navegador de confianza.</p></td></tr></table></td></tr></table></td></tr></table></td>
                    </tr></table><table class=""es-footer"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr><td align=""center""><table class=""es-footer-body"" width=""600"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center""><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""><table cellpadding=""0"" cellspacing=""0"" width=""100%""><tr><td width=""560"" align=""center"" valign=""top""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""center"" class=""es-p25"" bgcolor=""#cc0000""><p style=""color: #f9f7f7;line-height: 120%"">Text</p></td></tr></table></td></tr></table></td>
                    </tr><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p0r es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td>
                    <td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right""><tr><td width=""125"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td>
                    </tr><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p0r es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td>
                    <td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right""><tr><td width=""125"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td>
                    </tr><tr><td class=""es-p20t es-p20r es-p20l"" align=""left""> <!--[if mso]><table width=""560"" cellpadding=""0"" cellspacing=""0""><tr><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p0r es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td><td width=""145"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td><td class=""es-hidden"" width=""20""></td></tr></table> <!--[if mso]></td>
                    <td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left""><tr><td width=""125"" class=""es-m-p20b"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td><td width=""20""></td><td width=""125"" valign=""top""><![endif]--><table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right""><tr><td width=""125"" align=""center""><table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation""><tr><td align=""left""><p>Text</p></td></tr></table></td></tr></table> <!--[if mso]></td></tr></table><![endif]--></td></tr></table></td></tr></table></td></tr></table></div></body></html>


                    ";

            string bodyHTML2 = @"<h6>Referencia:</h6> <img src=""cid:Imagen"" width=""500"" height=""100"">";


            string bodyHTML3 = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" style=""font-family:arial, 'helvetica neue', helvetica, sans-serif"">
 <head>
  <meta charset=""UTF-8"">
  <meta content=""width=device-width, initial-scale=1"" name=""viewport"">
  <meta name=""x-apple-disable-message-reformatting"">
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
  <meta content=""telephone=no"" name=""format-detection"">
  <title>New Template</title><!--[if (mso 16)]>
    <style type=""text/css"">
    a {text-decoration: none;}
    </style>
    <![endif]--><!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--><!--[if gte mso 9]>
<xml>
    <o:OfficeDocumentSettings>
    <o:AllowPNG></o:AllowPNG>
    <o:PixelsPerInch>96</o:PixelsPerInch>
    </o:OfficeDocumentSettings>
</xml>
<![endif]--><!--[if !mso]><!-- -->
  <link href=""https://fonts.googleapis.com/css?family=Roboto:400,400i,700,700i"" rel=""stylesheet""><!--<![endif]-->
  <style type=""text/css"">
.rollover div {
	font-size:0;
}
.rollover:hover .rollover-first {
	max-height:0px!important;
	display:none!important;
}
.rollover:hover .rollover-second {
	max-height:none!important;
	display:block!important;
}
#outlook a {
	padding:0;
}
.es-button {
	mso-style-priority:100!important;
	text-decoration:none!important;
}
a[x-apple-data-detectors] {
	color:inherit!important;
	text-decoration:none!important;
	font-size:inherit!important;
	font-family:inherit!important;
	font-weight:inherit!important;
	line-height:inherit!important;
}
.es-desk-hidden {
	display:none;
	float:left;
	overflow:hidden;
	width:0;
	max-height:0;
	line-height:0;
	mso-hide:all;
}
[data-ogsb] .es-button {
	border-width:0!important;
	padding:10px 20px 10px 20px!important;
}
.es-button-border:hover a.es-button, .es-button-border:hover button.es-button {
	background:#56d66b!important;
	border-color:#56d66b!important;
}
.es-button-border:hover {
	border-color:#42d159 #42d159 #42d159 #42d159!important;
	background:#56d66b!important;
}
@media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150%!important } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120% } h1 { font-size:30px!important; text-align:left } h2 { font-size:24px!important; text-align:left } h3 { font-size:20px!important; text-align:left } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:30px!important; text-align:left } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:24px!important; text-align:left } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important; text-align:left } .es-menu td a { font-size:14px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:14px!important } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:14px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:14px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class=""gmail-fix""] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:inline-block!important } a.es-button, button.es-button { font-size:18px!important; display:inline-block!important } .es-adaptive table, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0px!important } .es-m-p0r { padding-right:0px!important } .es-m-p0l { padding-left:0px!important } .es-m-p0t { padding-top:0px!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important } table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important } .h-auto { height:auto!important } }
</style>

 </head>
 <body style=""width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
  <div class=""es-wrapper-color"" style=""background-color:#CFE2F3""><!--[if gte mso 9]>
			<v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
				<v:fill type=""tile"" color=""#cfe2f3""></v:fill>
			</v:background>
		<![endif]-->
   <table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#CFE2F3"">
     <tr>
      <td valign=""top"" style=""padding:0;Margin:0"">
       <table class=""es-content"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table class=""es-content-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
             <tr>
              <td align=""left"" style=""padding:0;Margin:0"">
               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" class=""es-m-txt-c"" bgcolor=""#b0dbfe"" style=""padding:40px;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px"">Mensaje enviado por correo electrónico automático <strong>NO RESPONDER</strong></p></td>
                     </tr>
                     <tr>
                      <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                       <table border=""0"" width=""50%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                         <tr>
                          <td style=""padding:0;Margin:0;border-bottom:0px solid #fff;background:unset;height:1px;width:100%;margin:0px""></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
             <tr>
              <td align=""left"" style=""padding:0;Margin:0""><!--[if mso]><table style=""width:800px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:294px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td class=""es-m-p0r es-m-p20b"" valign=""top"" align=""center"" style=""padding:0;Margin:0;width:294px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img class=""adapt-img"" src=""https://ziienz.stripocdn.email/content/guids/CABINET_0656a71faabe91d7b8b4b24dfc59f1d5/images/imagen1.gif"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""119""></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:486px"" valign=""top""><![endif]-->
               <table class=""es-right"" cellpadding=""0"" cellspacing=""0"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                 <tr>
                  <td align=""left"" style=""padding:0;Margin:0;width:486px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img class=""adapt-img"" src=""https://ziienz.stripocdn.email/content/guids/CABINET_0656a71faabe91d7b8b4b24dfc59f1d5/images/logo_teleperformance_original.png"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""281""></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td></tr></table><![endif]--></td>
             </tr>
             <tr>
              <td align=""left"" style=""padding:0;Margin:0"">
               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                       <table border=""0"" width=""50%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                         <tr>
                          <td style=""padding:0;Margin:0;border-bottom:0px solid #cccccc;background:unset;height:1px;width:100%;margin:0px""></td>
                         </tr>
                       </table></td>
                     </tr>
                     <tr>
                      <td align=""center"" style=""padding:25px;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:42px;color:#000000;font-size:35px""><strong>Historial Aleatoriedad</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
             <tr>
              <td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px"">
               <table width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td valign=""top"" align=""center"" style=""padding:0;Margin:0;width:760px"">
                   <table width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px"">Esta información fue enviada por medio de correo automático: No debe dar respuesta por ningún motivo, si no puede visualizar el contenido, intente ingresar desde un navegador de confianza.</p></td>
                     </tr>
                     <tr>
                      <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                       <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                         <tr>
                          <td style=""padding:0;Margin:0;border-bottom:0px solid #cccccc;background:unset;height:1px;width:100%;margin:0px""></td>
                         </tr>
                       </table></td>
                     </tr>
                     <tr>
                      <td align=""center"" style=""padding:20px;Margin:0;font-size:0"">
                       <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                         <tr>
                          <td style=""padding:0;Margin:0;border-bottom:0px solid #cccccc;background:unset;height:1px;width:100%;margin:0px""></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table bgcolor=""#efefef"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#efefef;width:800px"">
             <tr>
              <td align=""left"" style=""padding:0;Margin:0""><!--[if mso]><table style=""width:800px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:390px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td class=""es-m-p0r es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:390px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>

                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Referencia: </strong></p></td>
                     </tr>
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>ID CCMS: Evaluador: </strong></p></td>
                     </tr>
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Evaluador: </strong></p></td>
                     </tr>
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Fecha generada: </strong></p></td>
                     </tr>
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:roboto, 'helvetica neue', helvetica, arial, sans-serif;line-height:32px;color:#333333;font-size:16px""><strong>Cliente:</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:390px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                 <tr>
                  <td align=""center"" style=""padding:0;Margin:0;width:390px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                    ";
                            int con = 0;
                            foreach (var oItem in dataAleatorio)
                            {
                                con++;
                                if (con == 11)
                                {
                                    bodyHTML3 += @"<tr><td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:32px;color:#333333;font-size:16px"">"+oItem.ID_PDF2+"</p></td></tr>"+
                                    @"< tr >< td align = ""left"" style = ""padding: 0; Margin: 0"" >< p style = ""Margin: 0; -webkit - text - size - adjust:none; -ms - text - size - adjust:none; mso - line - height - rule:exactly; font - family:arial, 'helvetica neue', helvetica, sans - serif; line - height:32px; color:#333333;font-size:16px"">"+oItem.CCMS_EVALUATOR+"</p></td></ tr >"+
                                    @"< tr >< td align = ""left"" style = ""padding: 0; Margin: 0"" >< p style = ""Margin: 0; -webkit - text - size - adjust:none; -ms - text - size - adjust:none; mso - line - height - rule:exactly; font - family:arial, 'helvetica neue', helvetica, sans - serif; line - height:32px; color:#333333;font-size:16px"">"+oItem.Nombre+"</p></td></ tr >"+
                                    @"< tr >< td align = ""left"" style = ""padding: 0; Margin: 0"" >< p style = ""Margin: 0; -webkit - text - size - adjust:none; -ms - text - size - adjust:none; mso - line - height - rule:exactly; font - family:arial, 'helvetica neue', helvetica, sans - serif; line - height:32px; color:#333333;font-size:16px"">"+oItem.Rol+"</p></td></ tr >"+
                                    @"< tr >< td align = ""left"" style = ""padding: 0; Margin: 0"" >< p style = ""Margin: 0; -webkit - text - size - adjust:none; -ms - text - size - adjust:none; mso - line - height - rule:exactly; font - family:arial, 'helvetica neue', helvetica, sans - serif; line - height:32px; color:#333333;font-size:16px"">"+oItem.CRETE_DATETIME+"</p></td></ tr >"+
                                    @"< tr >< td align = ""left"" style = ""padding: 0; Margin: 0"" >< p style = ""Margin: 0; -webkit - text - size - adjust:none; -ms - text - size - adjust:none; mso - line - height - rule:exactly; font - family:arial, 'helvetica neue', helvetica, sans - serif; line - height:32px; color:#333333;font-size:16px"">"+oItem.Nombre_Campaña+"</p></td></ tr >" ;
                                }
                            }

                bodyHTML3 += @"                     
                   </table></td>
                 </tr>
               </table><!--[if mso]></td></tr></table><![endif]--></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table class=""es-footer"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table class=""es-footer-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
             <tr>
              <td align=""left"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:25px;padding-bottom:25px""><!--[if mso]><table style=""width:790px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td class=""es-m-p0r es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td align=""center"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:98px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:98px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:0px""></td><td style=""width:98px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                 <tr>
                  <td align=""left"" style=""padding:0;Margin:0;width:98px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td></tr></table><![endif]--></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
             <tr>
              <td align=""left"" style=""padding:0;Margin:0"">
               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0;font-size:0"">
                       <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                         <tr>
                          <td style=""padding:0;Margin:0;border-bottom:2px solid #999999;background:unset;height:1px;width:100%;margin:0px""></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table class=""es-footer"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table class=""es-footer-body"" cellspacing=""0"" cellpadding=""0"" bgcolor=""#ffffff"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
             <tr>
              <td align=""left"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:25px;padding-bottom:25px""><!--[if mso]><table style=""width:790px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td class=""es-m-p0r es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td class=""es-m-p20b"" align=""center"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td align=""center"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:99px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:99px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:98px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td align=""left"" class=""es-m-p20b"" style=""padding:0;Margin:0;width:98px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:0px""></td><td style=""width:98px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                 <tr>
                  <td align=""left"" style=""padding:0;Margin:0;width:98px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:17px;color:#333333;font-size:14px""><strong>Text</strong></p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td></tr></table><![endif]--></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
             <tr>
              <td align=""left"" style=""padding:0;Margin:0"">
               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:40px;Margin:0;font-size:0"">
                       <table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                         <tr>
                          <td style=""padding:0;Margin:0;border-bottom:0px solid #ffffff;background:unset;height:1px;width:100%;margin:0px""></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;border-top:2px solid #666666;border-right:2px solid #666666;border-left:2px solid #666666;width:800px;border-bottom:2px solid #666666"">
             <tr>
              <td align=""left"" style=""padding:0;Margin:0""><!--[if mso]><table style=""width:796px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:258px"" valign=""top""><![endif]-->
               <table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
                 <tr>
                  <td class=""es-m-p0r es-m-p20b"" valign=""top"" align=""center"" style=""padding:0;Margin:0;width:258px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0;font-size:0px""><img class=""adapt-img"" src=""https://ziienz.stripocdn.email/content/guids/CABINET_0656a71faabe91d7b8b4b24dfc59f1d5/images/imagen1.gif"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""200""></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:518px"" valign=""top""><![endif]-->
               <table class=""es-right"" cellpadding=""0"" cellspacing=""0"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
                 <tr>
                  <td align=""left"" style=""padding:0;Margin:0;width:518px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:24px;color:#333333;font-size:16px""><strong>Información Importante</strong></p></td>
                     </tr>
                     <tr>
                      <td align=""left"" style=""padding:0;Margin:0;padding-top:10px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px"">Información Importante</p></td>
                     </tr>
                   </table></td>
                 </tr>
               </table><!--[if mso]></td></tr></table><![endif]--></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
             <tr>
              <td align=""left"" style=""padding:0;Margin:0"">
               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td style=""padding:0;Margin:0"">
                       <table cellpadding=""0"" cellspacing=""0"" width=""100%"" class=""es-menu"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                         <tr class=""links"">
                          <td align=""center"" valign=""top"" width=""33%"" id=""esd-menu-id-0"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;border:0""><a target=""_blank"" href=""https://google.com"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#6FA8DC;font-size:14px"">Enlaces</a></td>
                          <td align=""center"" valign=""top"" width=""33%"" id=""esd-menu-id-1"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;border:0""><a target=""_blank"" href=""https://google.co"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#6FA8DC;font-size:14px"">Enlaces</a></td>
                          <td align=""center"" valign=""top"" width=""33%"" id=""esd-menu-id-2"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:10px;padding-bottom:10px;border:0""><a target=""_blank"" href=""https://google.co"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#6FA8DC;font-size:14px"">Enlaces</a></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table>
       <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
         <tr>
          <td align=""center"" style=""padding:0;Margin:0"">
           <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:800px"">
             <tr>
              <td align=""left"" style=""padding:0;Margin:0"">
               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:800px"">
                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                     <tr>
                      <td align=""center"" style=""padding:40px;Margin:0;font-size:0"">
                       <table cellpadding=""0"" cellspacing=""0"" class=""es-table-not-adapt es-social"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                         <tr>
                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:10px""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png"" alt=""Fb"" title=""Facebook"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:10px""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/twitter-logo-black.png"" alt=""Tw"" title=""Twitter"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:10px""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png"" alt=""Ig"" title=""Instagram"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                          <td align=""center"" valign=""top"" style=""padding:0;Margin:0""><img src=""https://ziienz.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png"" alt=""Yt"" title=""Youtube"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table></td>
                 </tr>
               </table></td>
             </tr>
           </table></td>
         </tr>
       </table></td>
     </tr>
   </table>
  </div>
 </body>
</html>";

            Speech_analytics.Models.EMAILINF obclsCorreo = new Speech_analytics.Models.EMAILINF
            {

                SERVIDOR = "smtp.office365.com",
                USUARIO = "jhonnn11livestar@hotmail.com",
                EMIALFROM = "jhonnn11livestar@hotmail.com",
                EMAILTO = "jhon.piedrahitahiguita@teleperformance.com",
                CONTRASEÑA = "Martha2002.",
                PUERTO = "587",
                AUTENTIFICACION = true,
                SEGURA = true,
                PRIORIADA = 0,
                TIPO = 1,
                ASUNTO = "APP",
                IMAGEN = Server.MapPath("~") + @"/Multime/EmailRandom/Ejemplo.gif",
                IDIMAGEN = "Imagen",
                MANSAJE = bodyHTML3

            };

            if (EmailClass.setEmail(obclsCorreo) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        [ActionName("SaveNewDataForPDF_DataBasic_Surveys")]
        public int SaveNewDataForPDF_DataBasic_Surveys(PDF_AL_USER oPdf_Al_USER)
        {

            if (RandomClass.SaveNewDataForPDF_Surveys(oPdf_Al_USER, Session["dataRamdomSurveys"]) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        [HttpPost]
        [ActionName("loadActGenerada")]
        public JsonResult loadActGenerada(string fechaInicial, string fechaFinal)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            loadCcmsInDataUserByUserRed.Process();
            int UserName = loadCcmsInDataUserByUserRed.Process();
            return Json(RandomClass.dataUserRamdomCallCustoms(UserName, fechaInicial, fechaFinal), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("loadActGeneradaS")]
        public JsonResult loadActGeneradaS(string fechaInicial, string fechaFinal)
        {
            string UserName = Session["idccms"].ToString();
            return Json(RandomClass.dataUserRamdomCallCustomsS(UserName, fechaInicial, fechaFinal), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [ActionName("loadIDPDF")]
        public void loadIDPDF(string idPDF)
        {
            Session["idPDF"] = idPDF;
        }

        [HttpGet]
        [ActionName("loadIDPDFS")]
        public void loadIDPDFS(string idPDF)
        {
            Session["idPDF"] = idPDF;
        }


        [HttpGet]
        [ActionName("loadPDFINF")]
        public ActionResult PDFAleatoriedad(string infPDF)
        {
            Session["infPDF"] = infPDF;

            ViewBag.Data = Session["infPDF"];

            return new Rotativa.ViewAsPdf(View())
            {
                PageOrientation = Rotativa.Options.Orientation.Landscape,

                FileName = "MiActividad.pdf"
            };


        }



        [HttpPost]
        [ActionName("loadActGeneradaForIDPDF")]
        public JsonResult loadActGeneradaForIDPDF()
        {
            return Json(PDFClass.dataPDF(Convert.ToString(Session["idPDF"])), JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //[ActionName("loadinfPDF")]
        //public JsonResult loadinfPDF()
        //{
        //    return Json(PDFClass.dataPDFAct(Convert.ToString(Session["idPDF"])), JsonRequestBehavior.AllowGet);
        //}


        [HttpGet]
        [ActionName("ConvetToPDFOnHTML")]
        public ActionResult ConvetToPDFOnHTML()
        {
            return new Rotativa.ViewAsPdf(View("PDF"))
            {
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
        }



        [HttpPost]
        [ActionName("verifyPermission")]
        public JsonResult verifyPermission(string cliente, string lob)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            return Json(RandomClass.verifyPermission(cliente, lob, Convert.ToString(CCMS)), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ActionName("loadccmsCliente")]
        public string loadccmsCliente(string IdCliente)
        {
            try
            {
                string ID_Cliente = AdminConfigureRandomClass.loadccmsCliente(Convert.ToString(IdCliente));

                if (ID_Cliente != null)
                {
                    return ID_Cliente;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        [ActionName("agentesFinales")]
        public JsonResult agentesFinales(string cliente, string filtro)
        {

            return Json(AppConfigureRandom.agentesFinales(cliente, filtro), JsonRequestBehavior.AllowGet);

        }

        public JsonResult analystsAssign2(MANAGER_CONFcs data)
        {

            return Json(AdminManager.analystsAssign2(data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("validarExisteFiltrosAleatoriedad")]
        public int validarExisteFiltrosAleatoriedad(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            return AppConfigureRandom.validarExisteFiltrosAleatoriedad(CCMS, data);
        }


        [HttpPost]
        [ActionName("loadCuotaAleatoriedad")]
        public int loadCuotaAleatoriedad(MANAGER_CONFIGURATION managerSentting)
        {
            BusinessLoadPromedioActividadByClienteLobMesAño businessLoadPromedioActividadByClienteLobMesAño = new BusinessLoadPromedioActividadByClienteLobMesAño(managerSentting);
            return businessLoadPromedioActividadByClienteLobMesAño.Process();
        }
    }
}















