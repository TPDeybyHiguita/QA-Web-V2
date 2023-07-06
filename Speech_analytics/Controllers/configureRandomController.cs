using Speech_analytics.Clases;
using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Speech_analytics.Controllers
{
    public class configureRandomController : Controller
    {

        [HttpPost]
        [ActionName("saveDataConfiguracionAleatoriedad")]
        public int saveDataConfiguracionAleatoriedad(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();

            int R;
            Dictionary<string, string> valuesForSql = new Dictionary<string, string>();
            string[] custom = data.CAMPAÑA.Split('-');
            valuesForSql.Add("@CAMPAÑA", Convert.ToString(custom[1]));
            valuesForSql.Add("@ID_CLIENTE", Convert.ToString(custom[0]));
            valuesForSql.Add("@FECHA_INICIAL", Convert.ToString(data.FECHA_INICIAL));
            valuesForSql.Add("@FECHA_FINAL", Convert.ToString(data.FECHA_FINAL));
            valuesForSql.Add("@NUMERO_MONITOREOS", Convert.ToString(data.NUMERO_MONITOREOS));
            valuesForSql.Add("@FECHA_ACTUALIZADO", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            valuesForSql.Add("@CCMS_ACTUALIZADO", Convert.ToString(CCMS));
            valuesForSql.Add("@LOB", Convert.ToString(data.LOB));
            valuesForSql.Add("@INICIO_LLAMADA_CORTA", Convert.ToString(data.INICIO_LLAMADA_CORTA));
            valuesForSql.Add("@FIN_LLAMADA_CORTA", Convert.ToString(data.FIN_LLAMADA_CORTA));
            valuesForSql.Add("@INICIO_LLAMADA_LARGA", Convert.ToString(data.INICIO_LLAMADA_LARGA));
            valuesForSql.Add("@FIN_LLAMADA_LARGA", Convert.ToString(data.FIN_LLAMADA_LARGA));
            valuesForSql.Add("@CCMS_ANALISTA", Convert.ToString(CCMS));
            valuesForSql.Add("@MES_ASIGNADO", Convert.ToString(data.MES_ASIGNADO));
            valuesForSql.Add("@AÑO_ASIGNADO", Convert.ToString(data.AÑO_ASIGNADO));
            valuesForSql.Add("@CSAT", Convert.ToString(data.CSAT));
            valuesForSql.Add("@NPS", Convert.ToString(data.NPS));
            valuesForSql.Add("@FCR", Convert.ToString(data.FCR));
            valuesForSql.Add("@CES", Convert.ToString(data.CES));
            valuesForSql.Add("@RECONTACTO", Convert.ToString(data.RECONTACTO));
            valuesForSql.Add("@HOLD_INICIAL", Convert.ToString(data.HOLD_INICIAL));
            valuesForSql.Add("@HOLD_FINAL", Convert.ToString(data.HOLD_FINAL));
            valuesForSql.Add("@AHT_INI", Convert.ToString(data.AHT_INI));
            valuesForSql.Add("@AHT_FIN", Convert.ToString(data.AHT_FIN));
            valuesForSql.Add("@TRASFERIDA", Convert.ToString(data.TRASFERIDA));

            if (AppConfigureRandom.validarExisteFiltrosAleatoriedad(CCMS, data) == 0)
            {

                return AppConfigureRandom.saveDataConfiguracionAleatoriedad(valuesForSql);

            }
            else
            {

                R = AppConfigureRandom.updateConfiguracionAleatoriedad(valuesForSql);
                
                
                if (AppConfigureRandom.validarExisteFiltrosAleatoriedadAgentes(R) != 0 )
                {
                    AppConfigureRandom.eliminarAnalistasEvaluar(R, CCMS);
                }

                return R;
            }
            
        }

        [HttpPost]
        [ActionName("saveConfiguracionAgentesEvaluar")]
        public bool saveConfiguracionAgentesEvaluar(EVALUAR_AGENTES data)
        {

            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();

            return AppConfigureRandom.saveAnalistasEvaluar(data, CCMS);

        }


        [HttpPost]
        [ActionName("loadingFilter")]
        public JsonResult loadingFilter(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();

            return Json(AdminConfigureRandomClass.loadingFilter(CCMS, data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("loadingFilterMiPlantilla")]
        public JsonResult loadingFilterMiPlantilla(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();

            return Json(AppConfigureRandom.loadingFilterMiPlantilla(CCMS, data), JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ActionName("loadCuotas")]
        public JsonResult loadCuotas(FILTER_RANDOM_CALL data)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            int cuotaDiaria = AppConfigureRandom.loadMesActividad(CCMS, data);
            int cuotaCumplida = AppConfigureRandom.loadDiaActividadCumplimiento(CCMS, data, Date);
            int cuotaFaltante = cuotaDiaria -  cuotaCumplida;

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
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
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
        [ActionName("loadAgentesEvaluar")]
        public JsonResult loadAgentesEvaluar(int idAgentesEvaluar)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            return Json(AppConfigureRandom.loadAgentesEvaluar(idAgentesEvaluar, CCMS), JsonRequestBehavior.AllowGet);
        }


    }
}