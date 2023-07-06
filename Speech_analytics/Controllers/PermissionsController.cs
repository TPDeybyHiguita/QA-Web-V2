using Speech_analytics.Clases;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Speech_analytics.Controllers
{
    public class PermissionsController : Controller
    {
        // GET: Permissions



        [HttpPost]
        [ActionName("validarPermisosUser")]
        public JsonResult validarPermisosUser()
        {
            List<PERMISOS_USERcs> list = new List<PERMISOS_USERcs>();

                list.Add(new PERMISOS_USERcs
                {
                    createUser = Convert.ToInt32(Session["createUser"]),
                    editPermissions = Convert.ToInt32(Session["editPermissions"]),
                    AddCampaigns = Convert.ToInt32(Session["AddCampaigns"]),
                    myTeams = Convert.ToInt32(Session["myTeams"]),
                    randomnessResults = Convert.ToInt32(Session["randomnessResults"]),
                    random = Convert.ToInt32(Session["random"]),
                    planCalculator = Convert.ToInt32(Session["planCalculator"]),
                    daysCalculator = Convert.ToInt32(Session["daysCalculator"]),
                    bitacoraSura = Convert.ToInt32(Session["bitacoraSura"])

                });
            

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}