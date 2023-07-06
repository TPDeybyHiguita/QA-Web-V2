using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class FILTER_RANDOM_CALL
    {
        
        public int NUMERO_MONITOREOS { get; set; }
        public int CCMS_CLIENTE { get; set; }
        public string CCMS_ANALISTA { get; set; }
        public int CCMS_ACTUALIZADO { get; set; }
        public string ID_CLIENTE { get; set; }
        public string CAMPAÑA { get; set; }
        public string SKILL_INICIAL { get; set; }
        public string SKILL_FINAL { get; set; }
        public string TIPOLOGIA { get; set; }
        public string NOMBRE_ACTUALIZO { get; set; }
        public string AGENTE { get; set; }
        public string FECHA_INICIAL { get; set; }
        public string LOB { get; set; }
        public string FECHA_FINAL { get; set; }
        public string FECHA_ACTUALIZADO { get; set; }
        public string INICIO_LLAMADA_CORTA { get; set; }
        public string FIN_LLAMADA_CORTA { get; set; }
        public string INICIO_LLAMADA_LARGA { get; set; }
        public string FIN_LLAMADA_LARGA { get; set; }
        public string AGENTE_EVALUAR { get; set; }
        public string MES_ASIGNADO { get; set; }
        public string AÑO_ASIGNADO { get; set; }
        public string FECHA { get; set; }
        public string TRASFERIDA { get; set; }
        public string RECONTACTO { get; set; }
        public string CSAT { get; set; }
        public string NPS { get; set; }
        public string FCR { get; set; }
        public string CES { get; set; }
        public string HOLD_INICIAL { get; set; }
        public string HOLD_FINAL { get; set; }
        public int TIPO_LLAMADAS { get; set; }
        public string FILTRO_FECHA { get; set; }
        public string FILTRO_NIVEL { get; set; }
        public string AHT_INI { get; set; }
        public string AHT_FIN { get; set; }
    }
}