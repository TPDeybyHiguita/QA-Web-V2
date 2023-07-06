using System;

namespace Speech_analytics.Models
{
    public class TBL_FactEmpleados
    {
        public int idccms { get; set; }
        public string Nombre { get; set; }
        public string direccion { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public DateTime fechanacimiento { get; set; }
        public string Sexo { get; set; }
        public string tipoidfiscal { get; set; }
        public string idfiscal { get; set; }
        public string email { get; set; }
        public string Estado { get; set; }
        public string horario { get; set; }
        public string telefono { get; set; }
        public string tiporemuneracion { get; set; }
        public string beneficio { get; set; }
        public string idcuenta { get; set; }
        public DateTime fechacontratacion { get; set; }
        public DateTime fechabaja { get; set; }
        public int idmanager { get; set; }
        public string NombreManager { get; set; }
        public string Programa { get; set; }
        public string Cliente { get; set; }
        public string SiTE { get; set; }
        public int Login { get; set; }
        public DateTime InicioLogin { get; set; }
        public DateTime FinLogin { get; set; }
        public DateTime FechaUltActividad { get; set; }
        public string Puesto { get; set; }
        public string Rol { get; set; }
        public string UsuarioCRM { get; set; }
        public string MERCADO { get; set; }

    }
}