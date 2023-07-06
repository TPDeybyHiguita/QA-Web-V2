using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class TBL_Bitacora_Actualizaciones_arlcs
    {
        public string ID_CASO { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string MOTIVO_1 { get; set; }
        public string COMENTARIO { get; set; }
        public string FECHA_CREACION { get; set; }
        public string ESTADO { get; set; }
        public int AGENTES_IMPACT_CITA { get; set; }
        public int AGENTES_IMPACT_URG { get; set; }
        public string FECHA_CERTIFI_CITA { get; set; }
        public string FECHA_CERTIFI_URG { get; set; }
        public string MED_ACT_INFO_CITA { get; set; }
        public string MED_ACT_INFO_URG { get; set; }
        public string MED_SOCIAL_CITA { get; set; }
        public string MED_SOCIAL_URG { get; set; }
        public string SERVICIO { get; set; }
    }
}