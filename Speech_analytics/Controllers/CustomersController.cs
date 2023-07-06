using Speech_analytics.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Speech_analytics.Controllers
{
    public class CustomersController : Controller
    {

        [HttpPost]
        [ActionName("saveConfCliente")]
        public bool saveConfCliente(string cliente)
        {
            return CustomersClass.saveConfCliente(cliente);
        }

        [HttpPost]
        [ActionName("updateConfClienteInactivar")]
        public bool updateConfClienteInactivar(int idCliente)
        {
            return CustomersClass.updateConfClienteInactivar(idCliente);
        }

        [HttpPost]
        [ActionName("updateConfClienteActivar")]
        public bool updateConfClienteActivar(int idCliente)
        {
            return CustomersClass.updateConfClienteActivar(idCliente);
        }

        [HttpPost]
        [ActionName("dataCliente")]
        public JsonResult dataCliente()
        {
            return Json(CustomersClass.dataCliente(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("loadListClient")]
        public JsonResult loadListClient()
        {
            return Json(CustomersClass.loadClientes(), JsonRequestBehavior.AllowGet);
        }

    }
}