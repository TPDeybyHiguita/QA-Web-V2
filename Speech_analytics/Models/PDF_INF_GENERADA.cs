using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class PDF_INF_GENERADA
    {
        public string ID_PDF { get; set; }
        public int CCMS_EVALUATOR { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string CRETE_DATETIME { get; set; }
        public string Nombre_Campaña { get; set; }
    }
}