using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace Speech_analytics.Clases
{
    public class sendEmailNewUser
    {
        public static int setEmail(Models.EMAILINF obclsCorreo)
        {

            try
            {
                MailMessage Mail = new MailMessage();

                Mail.From = new MailAddress(obclsCorreo.EMIALFROM);
                Mail.To.Add(obclsCorreo.EMAILTO);
                Mail.Subject = obclsCorreo.ASUNTO;
                Mail.Body = obclsCorreo.MANSAJE;

                if (obclsCorreo.TIPO == 0) Mail.IsBodyHtml = false;
                else if (obclsCorreo.TIPO == 1) Mail.IsBodyHtml = true;

                if (obclsCorreo.PRIORIADA == 2) Mail.Priority = MailPriority.High;
                else if (obclsCorreo.PRIORIADA == 1) Mail.Priority = MailPriority.Low;
                else if (obclsCorreo.PRIORIADA == 0) Mail.Priority = MailPriority.Normal;

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(obclsCorreo.MANSAJE,
                    Encoding.UTF8,
                    MediaTypeNames.Text.Html);

                //incrustando una imagen
                LinkedResource img = new LinkedResource(obclsCorreo.IMAGEN, MediaTypeNames.Image.Gif);
                img.ContentId = obclsCorreo.IDIMAGEN;
                htmlView.LinkedResources.Add(img);

                Mail.AlternateViews.Add(htmlView);

                //cliente de servidor de correo
                SmtpClient smtp = new SmtpClient
                {
                    Host = obclsCorreo.SERVIDOR
                };

                if (obclsCorreo.AUTENTIFICACION)
                {
                    smtp.Credentials = new System.Net.NetworkCredential(obclsCorreo.USUARIO, obclsCorreo.CONTRASEÑA);
                }

                if (obclsCorreo.PUERTO.Length > 0)
                {
                    smtp.Port = Convert.ToInt32(obclsCorreo.PUERTO);
                }

                smtp.EnableSsl = obclsCorreo.SEGURA;

                smtp.Send(Mail);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public static USERDATA loadDataEmailNewUser(int CCMS)
        {

            Connection.oConnection.Close();
            Connection.oConnection.Open();


            SqlCommand oQueryList = new SqlCommand("SELECT INFO.EMAIL, INFO.NOMBRES, INFO.APELLIDOS, CREDENCIALES.ID_USER, CREDENCIALES.PASSWORD_USER, INFO.EMAIL_ALTERNO " +
                                                    "FROM AppSA.USERDATA AS INFO " +
                                                    "INNER JOIN [HC].[TBL_FactEmpleados] AS EMPLEADOS ON INFO.CCMS = EMPLEADOS.idccms " +
                                                    "INNER JOIN AppSA.USERS AS CREDENCIALES ON EMPLEADOS.idcuenta = CREDENCIALES.ID_USER " +
                                                    "WHERE INFO.CCMS = '"+CCMS+"'", Connection.oConnection);
            SqlDataReader oRead = oQueryList.ExecuteReader();

            USERDATA userData = new USERDATA();


            if (oRead.Read())
            {
                userData.NOMBRES = Convert.ToString(oRead["NOMBRES"]);
                userData.APELLIDOS = Convert.ToString(oRead["APELLIDOS"]);
                userData.EMAIL = Convert.ToString(oRead["EMAIL"]);
                userData.EMAIL_ALTERNO = Convert.ToString(oRead["EMAIL_ALTERNO"]);
                userData.SessionModel = new SessionModel
                {
                    UserName = Convert.ToString(oRead["ID_USER"]),
                    UserPassword = Convert.ToString(oRead["PASSWORD_USER"]),
                };




            }
            
            return userData;
        }
















    }
}