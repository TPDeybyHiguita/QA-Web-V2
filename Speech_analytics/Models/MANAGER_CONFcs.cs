
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class MANAGER_CONFcs
    {
        public string CCMS_MANAGER { get; set; }
        public string CLIENTE { get; set; }
        public string LOB { get; set; }
        public string NUM_ANALISTAS { get; set; }
        public string NUM_MONITOREOS { get; set; }
        public string MES_ASIGNADO { get; set; }
        public string AÑO_ASIGNADO { get; set; }
        public string CUOTA_MENSUAL { get; set; }
        public string DIAS_ACTIVIDAD { get; set; }
        public DateTime FECHA_ACTUALIZADO { get; set; }
        public string CCMS_ANALISTA { get; internal set; }
        public string CUOTA_CUMPLIDA { get; internal set; }
    }
}