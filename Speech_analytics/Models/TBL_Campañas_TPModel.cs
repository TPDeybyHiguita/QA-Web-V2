namespace Speech_analytics.Models
{
    public class TBL_Campañas_TPModel
    {
        public string ID_Cliente { get; set; }
        public string Mercado { get; set; }
        public string Ciudad { get; set; }
        public string Site { get; set; }
        public int ID_CCMS_Director { get; set; }
        public int ID_CCMS_Manager { get; set; }
        public string ID_CCMS_Campaña { get; set; }
        public string Nombre_Campaña { get; set; }
        public string Nombre_Abreviatura { get; set; }
        public int CodClienteCCMS { get; set; }
        public string BaseDatos { get; set; }
        public string SP_TPCLIENT { get; set; }
        public string NombreCalidad { get; set; }
    }
}