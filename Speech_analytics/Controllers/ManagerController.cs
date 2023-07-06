using Speech_analytics.Business;
using Speech_analytics.Clases;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Speech_analytics.Controllers
{
    public class ManagerController : Controller
    {

        [HttpPost]
        [ActionName("newAnalyst")]
        public ActionResult newAnalyst(MANAGER_ANALISTA data)
        {
            BusinessAddAnyst businessAddAnyst = new BusinessAddAnyst(data, Convert.ToString(Session["UserName"]));
            return Json(businessAddAnyst.Process());
        }
    }
}