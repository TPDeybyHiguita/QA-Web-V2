using Speech_analytics.Business;
using Speech_analytics.Clases;
using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Speech_analytics.Controllers
{
    public class UserController : Controller
    {
        // GET: ProfileUpdate
        public ActionResult UpdateProfile()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ActionName("LoandDataUser")]
        public ActionResult LoandDataUser()
        {
            LoadDataUserByUserRedInDataUser loadDataUserByUserRedInDataUser = new LoadDataUserByUserRedInDataUser(Convert.ToString(Session["UserName"]));

            return Json(loadDataUserByUserRedInDataUser.Process());
        }

        [HttpPost]
        [ActionName("actualizarDataUser")]
        public JsonResult actualizarDataUser(USERDATA dataUser)
        {
            ProfileClass.actualizarDataUser(dataUser, Convert.ToString(Session["UserName"]));
            return Json(ProfileClass.dataUser(Convert.ToString(Session["UserName"])), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("actualizarDataUserPassword")]
        public int actualizarDataUserPassword(SessionModel password)
        {
            if (ProfileClass.actualizarDataUserPassword(password, Convert.ToString(Session["UserName"])) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            
        }

        [HttpPost]
        [ActionName("mayusculasNum")]
        public int mayusculasNum(string cadena)
        {

            return AdminNewUser.mayusculasNum(cadena);
        }









    }
}