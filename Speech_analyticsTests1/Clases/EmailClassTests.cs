using Microsoft.VisualStudio.TestTools.UnitTesting;
using Speech_analytics.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speech_analytics.Clases.Tests
{
    [TestClass()]
    public class EmailClassTests
    {
        [TestMethod()]
        public void setEmailTest()
        {
            Speech_analytics.Models.EMAILINF obclsCorreo = new Speech_analytics.Models.EMAILINF
            {

                SERVIDOR = "mail.teleperformance.co",
                USUARIO = "Qawebpanamericano",
                EMIALFROM = "qawebpanamericano@teleperformance.co",
                EMAILTO = "peraltalondonoalex@gmail.com",
                CONTRASEÑA = "d&2f8q!@6UDbGnKN",
                PUERTO = "25",
                AUTENTIFICACION = true,                
                SEGURA = false,
                PRIORIADA = 0,
                TIPO = 1,
                ASUNTO = "ALERTA DRILL DOWN REPORTS -- ACTIVIDAD GENERADA PARA CCMS: ",
                IMAGEN = "none",
                IDIMAGEN = "Imagen",
                MANSAJE = "HOla",

            };

            EmailClass.setEmail(obclsCorreo);
        }
    }
}