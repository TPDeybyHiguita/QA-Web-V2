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
    public class RandomController : Controller
    {

        [HttpPost]
        [ActionName("loadingFilter")]
        public JsonResult loadingFilter(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();

            return Json(AdminConfigureRandomClass.loadingFilter(CCMS, data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("actividadLlamadas")]
        public JsonResult actividadLlamadas(String IDPDF)
        {

            return Json(myActividadLlamadas.actividadLlamadas(IDPDF), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("actualizarEstadoMonitoreo")]
        public bool actualizarEstadoMonitoreo(string REFERENCIA, int ID, string OBSERVACION, string ESTADO)
        {

            return myActividadLlamadas.actualizarEstadoMonitoreo(REFERENCIA, ID, OBSERVACION, ESTADO);
        }

        [HttpPost]
        [ActionName("loadDataActividad")]
        public JsonResult loadDataActividad(string idActividad)
        {

            return Json(myActividadLlamadas.loadDataActividad(idActividad),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("loadLobCliente")]
        public string loadLobCliente(int skill)
        {

            return RandomClass.loadLobCliente(skill);

        }


        [HttpPost]
        [ActionName("loadCuotaCumplidaDiaria")]
        public int loadCuotaCumplidaDiaria(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            int cuotaDiaria = AppConfigureRandom.loadMesActividad(CCMS, data);
            int cuotaCumplida = AppConfigureRandom.loadDiaActividadCumplimiento(CCMS, data, Date);
            int cuotaFaltante = cuotaDiaria - cuotaCumplida;

            return cuotaFaltante;
        }













    }
}