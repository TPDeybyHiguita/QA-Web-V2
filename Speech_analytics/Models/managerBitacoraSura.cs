using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class managerBitacoraSura
    {
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdCcms { get; set; }
        public DateTime FechaActualizado { get; set; }
        public string Estado { get; set; }
        public string UsuarioRed { get; set; }
    }
}