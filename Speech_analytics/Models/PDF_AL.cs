using System;
namespace Speech_analytics.Models
{
    public class PDF_AL
    {
        public int ID { get; set; }
        public int POSITION_PDF { get; set; }
        public int ID_PDF { get; set; }
        public int AGENTE_INICIAL { get; set; }
        public int TALK_TIME { get; set; }
        public int HOLD_TIME { get; set; }
        public int TIMES_HELD { get; set; }
        public int DURACION { get; set; }
        public string ORIGEN_COLGADA { get; set; }
        public string TRANSFERIDA { get; set; }
        public string CAMPAÑA { get; set; }
        public string UCID { get; set; }
        public string CUMPLIMIENTO { get; set; }
        public string OBSERVACION { get; set; }
        public string NOMBRE_AGENTE { get; set; }

    }
}