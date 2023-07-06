using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class TBL_IVR_Resolutivo
    {
        
        public string Encuesta { get; set; }
        public string ID_Encuesta { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public string UCID { get; set; }
        public string Documento_Cliente { get; set; }
        public string Extension { get; set; }
        public string Split { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public string Fecha_Fin { get; set; }
        public int ID { get; set; }
        public string Canalcall { get; set; }
        public string Login { get; set; }
        public string ANI { get; set; }
        public string Llave { get; set; }
        public string Campaña { get; set; }
        public string Llave_Sociodemografico { get; set; }
        public string Origen_Pregunta { get; set; }
        public string STRPregunta { get; set; }
        public string Tipo_Pregunta { get; set; }


    }
}