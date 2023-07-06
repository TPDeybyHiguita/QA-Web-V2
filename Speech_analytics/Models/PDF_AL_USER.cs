using System;

namespace Speech_analytics.Models
{
    public class PDF_AL_USER
    {
        public string ID_PDF { get; set; }
        public int CCMS_EVALUATOR { get; set; }
        public string CRETE_DATETIME { get; set; }
        public string ID_CLIENTE { get; set; }
        public string LOB { get; set; }
        public USERDATA USERDATA { get; internal set; }
        public TBL_Info_Clientes TBL_Info_Clientes { get; internal set; }
    }
}