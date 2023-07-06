using Speech_analytics.Clases;
using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Speech_analytics.Controllers
{
    public class AdminResultsRandomController : Controller
    {
        [HttpPost]
        [ActionName("loadDataUser")]
        public JsonResult agregarAnalista(string CCMS)
        {
            return Json(AdminManager.newAnalista(CCMS), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("dataUserRamdomCallCustoms")]
        public JsonResult dataUserRamdomCallCustoms(string CCMS, string cliente, string lob, string fechaInicial, string fechaFinal)
        {
            return Json(AppResultsRandom.dataUserRamdomCallCustoms(CCMS, cliente, lob, fechaInicial, fechaFinal), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("loadCuotas")]
        public JsonResult loadCuotas(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(data.CCMS_ANALISTA);
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            int cuotaDiaria = AppConfigureRandom.loadMesActividad(CCMS, data);
            int cuotaCumplida = AppConfigureRandom.loadDiaActividadCumplimiento(CCMS, data, Date);
            int cuotaFaltante = cuotaDiaria - cuotaCumplida;

            List<string> cuotas = new List<string>
            {
                Convert.ToString(cuotaDiaria),
                Convert.ToString(cuotaCumplida),
                Convert.ToString(cuotaFaltante)
            };

            return Json(cuotas, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("loadCuotasMensual")]
        public JsonResult loadCuotasMensual(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(data.CCMS_ANALISTA);
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            string Date = DateTime.Now.ToString("yyyy-MM");

            int cuotaMensual = AppConfigureRandom.loadActividadMensual(CCMS, data);
            int cuotaCumplida = AppConfigureRandom.loadActividadRealizadaMes(CCMS, data, Date);
            int cuotaFaltante = cuotaMensual - cuotaCumplida;

            List<string> cuotas = new List<string>
            {
                Convert.ToString(cuotaMensual),
                Convert.ToString(cuotaCumplida),
                Convert.ToString(cuotaFaltante)
            };

            return Json(cuotas, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("loadListManager")]
        public JsonResult loadListManager()
        {

            return Json(AppConfigureRandom.loadListManager(Convert.ToString(Session["UserName"])), JsonRequestBehavior.AllowGet);
        }





    }





}