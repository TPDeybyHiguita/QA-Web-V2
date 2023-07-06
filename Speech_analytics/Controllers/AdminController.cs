using Speech_analytics.Business;
using Speech_analytics.Clases;
using Speech_analytics.Data;
using Speech_analytics.Models;
using Speech_analytics.Models.States;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Speech_analytics.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult NewUser()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo" && Session["createUser"].ToString() == "1")
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

        public ActionResult Manager()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo" && Session["myTeams"].ToString() == "1")
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

        public ActionResult ConfigureRandomness()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo" && Session["random"].ToString() == "1")
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

        public ActionResult ResultsRandom()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo" && Session["randomnessResults"].ToString() == "1")
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


        public ActionResult UserPermissions()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo" && Session["editPermissions"].ToString() == "1")
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

        public ActionResult Customers()
        {
            try
            {
                if (Session["Estatus"].ToString() == "Activo" && Session["AddCampaigns"].ToString() == "1")
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
        [ActionName("LoginOff")]
        public ActionResult LoginOff()
        {
            Session["UserName"] = null;
            Session["UserPassword"] = null;
            Session["Estatus"] = "Inactiva";
            Session["ALEATORIEDAD"] = null;
            Session["CALCULADORAS"] = null;
            Session["ADMINISTRADOR"] = null;
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        [ActionName("LoandListCampaign")]
        public JsonResult LoandListCampaign()
        {
            return Json(RandomClass.lsClientes(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("loadccmsCliente")]
        public string loadccmsCliente(string IdCliente)
        {
            try
            {
                string ID_Cliente = AdminConfigureRandomClass.loadccmsCliente(Convert.ToString(IdCliente));

                if (ID_Cliente != null)
                {
                    return ID_Cliente;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        [ActionName("updateRandom")]
        public int updateRandom(FILTER_RANDOM_CALL data)
        {

            if (AdminConfigureRandomClass.updateRandom(data, Convert.ToString(Session["UserName"])) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

  

        [HttpPost]
        [ActionName("OnChangeLsCliente")]
        public JsonResult OnChangeLsCliente(string IdCliente)
        {
            return Json(RandomClass.lsSubClientesFromCliente(IdCliente), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("newAnalista")]
        public JsonResult newAnalista(string CCMS)
        {
            return Json(AdminManager.newAnalista(CCMS), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("agregarAnalista")]
        public JsonResult agregarAnalista(string USER_RED)
        {
            return Json(AdminManager.newAnalista(USER_RED), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("listAnalysts")]
        public JsonResult listAnalysts()
        {
            return Json(AdminManager.listAnalysts(Convert.ToString(Session["UserName"])), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("lodAnalys")]
        public JsonResult lodAnalys(string CCMS_ANALISTA)
        {
            return Json(AdminManager.loadAnalys(Convert.ToString(Session["UserName"]), CCMS_ANALISTA), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("updateLob")]
        public JsonResult updateLob(int CCMS_LOB)
        {
            return Json(AdminManager.updateLob(CCMS_LOB), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("activeLob")]
        public JsonResult activeLob(int CCMS_LOB)
        {
            return Json(AdminManager.activeLob(CCMS_LOB), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("newLob")]

        public ActionResult newLob(MANAGER_ANALISTA managerAnalista)
        {

            BusinessAddLobAnalys businessAddLobAnalys = new BusinessAddLobAnalys(managerAnalista, Convert.ToString(Session["UserName"]));
            return Json(businessAddLobAnalys.Process(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("updateStatus")]

        public int updateStatus(int CCMS_ANALISTA)
        {

            return AdminManager.updateStatus( CCMS_ANALISTA, Convert.ToString(Session["UserName"]));

        }

        [HttpPost]
        [ActionName("numDays")]

        public int numDays(int MES, int AÑO)
        {

            int numDays = AdminManager.numDays(MES,AÑO);

            return numDays;
        }

        public int analystsAssign(MANAGER_CONFIGURATION namagerSentting)
        {
            return AdminManager.analystsAssign(namagerSentting);
        }

        [HttpPost]
        [ActionName("saveConfManager")]

        public ActionResult saveConfManager(MANAGER_CONFIGURATION managerSentting)
        {
            BusinessSenttingManager businessSenttingManager = new BusinessSenttingManager(managerSentting, Convert.ToString(Session["UserName"]));
            return Json(businessSenttingManager.Process(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult laodDataManagerSetting(MANAGER_CONFIGURATION managerSetting)
        {
            LoadDataManagerSetting loadDataManagerSetting = new LoadDataManagerSetting(managerSetting);
            return Json(loadDataManagerSetting.Process(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("loadCuotaCumplidaMes")]
        public int loadCuotaCumplidaMes(MANAGER_CONFcs data)
        {

            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(Convert.ToString(Session["UserName"]));
            int CCMS = loadCcmsInDataUserByUserRed.Process();
            string Date = DateTime.Now.ToString("yyyy-MM");
            int cuotaCumplida = AppConfigureRandom.loadCuotaCumplidaMes(CCMS, data, Date);

            return cuotaCumplida;
        }

        [HttpPost]
        [ActionName("loadAnalysLob")]
        public JsonResult loadAnalysLob(string CLIENTE, string LOB)
        {
            return Json(AdminConfigureRandomClass.loadAnalysLob(CLIENTE, LOB, Convert.ToString(Session["UserName"])), JsonRequestBehavior.AllowGet);
        }

        [ActionName("LoandName")]
        public JsonResult LoandName()
        {
            List<SessionModel> oDataSession = new List<SessionModel>();
            oDataSession.Add(new SessionModel
            {
                UserName = Session["UserName"].ToString(),
                Estatus = Session["Estatus"].ToString(),
                LastLogin = Session["LastLogin"].ToString(),
                Nombre = Session["Nombre"].ToString(),
            });
            return Json(oDataSession, JsonRequestBehavior.AllowGet);
        }


        //********************** PETICIONES DE NUW USUARIOS **********************

        [HttpPost]
        [ActionName("loadDataUser")]
        public JsonResult loadDataUser(string USER_RED)
        {
            LoadUserDataByUserRedInHC loadUserDataByUserRedInHC = new LoadUserDataByUserRedInHC(USER_RED);       
            return Json(loadUserDataByUserRedInHC.Process(), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("validateExisUser")]
        public int validateExisUser (USERDATA data)
        {

            if (AdminNewUser.validateExisUser(data.CCMS) == 1)
            {
                int R= AdminNewUser.saveDataUser(data);

                if (R == 1)
                {
                    int RE = AdminNewUser.saveDataUserSession(data);

                    return 1;
                }
                else
                {
                    return 0;
                }                
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

        //*************************USERPERMISSIONS ************************

        [HttpPost]
        [ActionName("loadUser")]
        public JsonResult loadUser(int CCMS)
        {
            return Json(userPermissions.loadUser(CCMS), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("loadUserPermissions")]
        public JsonResult loadUserPermissions(string userRed)
        {
            LoadUserPermissionsByUserRed loadUserPermissionsByUserRed = new LoadUserPermissionsByUserRed(userRed);
            return Json(loadUserPermissionsByUserRed.Process(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("updatePermissionsUser")]
        public JsonResult updatePermissionsUser(PERMISOS_USERcs permissionsUser)
        {
            UpdatePermisionssUserByUserRed updatePermisionssUserByUserRed = new UpdatePermisionssUserByUserRed(permissionsUser);

            return Json(updatePermisionssUserByUserRed.Process(),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("updateUserData")]
        public JsonResult updateUserData(USERDATA userData)
        {
            UpdateDataUser updateDataUser = new UpdateDataUser(userData);
            return Json(updateDataUser.Process(), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("loadDataUserByUserRed")]
        public JsonResult loadDataUserByUserRed(string userRed)
        {
            LoadDataUserByUserRedInDataUser loadDataUserByUserRedInDataUser = new LoadDataUserByUserRedInDataUser(userRed);
            return Json(loadDataUserByUserRedInDataUser.Process(), JsonRequestBehavior.AllowGet);
        }

    }
}


