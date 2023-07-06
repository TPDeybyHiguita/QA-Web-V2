using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class EVALUAR_AGENTES
    {
        public int ID_AGENTES { get; set; }
        public string NOMBRE_AGENTE { get; set; }
        public int CCMS_AGENTE { get; set; }
        public int NUMERO_AGENTES { get; set; }
        public string LOGIN_AGENTE { get; set; }
        public DateTime FECHA_ACTUALIZADO { get; set; }
        public int CCMS_ACTUALIZADO { get; set; }
        public int NUMERO_MONITOREOS { get; set; }
        public  TBL_FactEmpleados tbl_factempleados{ get; internal set; }
    }
}