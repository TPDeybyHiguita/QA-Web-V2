using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class MANAGER_CONFIGURATION
    {
        public string UserRedManager { get; set; }
        public string CcmsManager { get; set; }
        public string Cliente { get; set; }
        public int? IdCliente { get; set; }
        public string Lob { get; set; }
        public string IdLob { get; set; }
        public int? NumAnalistas { get; set; }
        public int? Mes { get; set; }
        public int? Año { get; set; }
        public int? CuotaMensual { get; set; }
        public int? DiasActividad { get; set; }
        public int? PromedioActividad { get; set; }
        public DateTime? FechaActualizado { get; set; }
    }
}