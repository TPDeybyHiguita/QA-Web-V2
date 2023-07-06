using System;

namespace Speech_analytics.Models
{
    public class TBL_ECH
    {
        public TBL_FactEmpleados TBL_FactEmpleados { get; set; }
        public string CALLSEGMENTFACTKEY { get; set; }
        public int CONCTACT_ID { get; set; }
        public DateTime FECHA_INICIAL { get; set; }
        public DateTime FECHA_FINAL { get; set; }
        public string UCID { get; set; }
        public int SKILL { get; set; }
        public string NOMBRE_SKILL_AVAYA { get; set; }
        public string NOMBRE_SKILL_CALIDAD { get; set; }
        public int VDN { get; set; }
        public string NOMBRE_VDN { get; set; }
        public int VECTOR { get; set; }
        public string NOMBRE_VECTOR { get; set; }
        public int SKILL_LEVEL { get; set; }
        public int AGENTE_FINAL { get; set; }
        public string N_AGENTE_FINAL { get; set; }
        public int AGENTE_INICIAL { get; set; }
        public string N_AGENTE_INICIAL { get; set; }
        public string TIPO_LLAMADA { get; set; }
        public string DIALED_NUMBER { get; set; }
        public string CALLING_PARTY { get; set; }
        public int QUEUE_TIME { get; set; }
        public int TALK_TIME { get; set; }
        public int ACW_TIME { get; set; }
        public int HOLD_TIME { get; set; }
        public int TIMES_HELD { get; set; }
        public int DISPOSITION_TIME { get; set; }
        public int CONSULT_TIME { get; set; }
        public int RING_TIME { get; set; }
        public int DURACION { get; set; }
        public int SEGMENTO_LLAMADA { get; set; }
        public string ORIGEN_COLGADA { get; set; }
        public string TRANSFERIDA { get; set; }
        public int JUNK_KEY { get; set; }
        public string PULL_REASON { get; set; }
        public string PRIORIDAD_LLAMADA { get; set; }
        public string REGION { get; set; }
        public int META { get; set; }
        public string CAMPAÑA { get; set; }
        public string LOB { get; set; }
        public string CMS { get; set; }
        public int VALIDACION { get; set; }
        public int SKILL_DESTINO { get; set; }
        public string NOMBRE_SKILL_AVAYA_DESTINO { get; set; }
        public string NOMBRE_SKILL_CALIDAD_DESTINO { get; set; }
    }
}