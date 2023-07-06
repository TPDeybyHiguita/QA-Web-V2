using Speech_analytics.Clases;
using Speech_analytics.Models;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using Speech_analytics.Data;

namespace Speech_analytics.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            try
            {
                if (Session["Estatus"].ToString() != "Activo")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "App");
                }
                }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ActionName("validateUser")]
        public int validateUser(DirectoryActive directoryActive)
        {
            try
            {
                bool userExistDirectoryActive;
                AzureADAuthenticator azureADAuthenticator = new AzureADAuthenticator(directoryActive);
                UpdateLastLoginByUser updateLastLoginByUser = new UpdateLastLoginByUser(directoryActive.user);
                LoginClass loginClass = new LoginClass(directoryActive);

                userExistDirectoryActive = azureADAuthenticator.Autenticar();

                if (userExistDirectoryActive == true)
                {
                    SqlDataReader oResult = loginClass.Process();

                    if (oResult != null)
                    {
                        
                        Session["UserName"] = Convert.ToString(directoryActive.user);
                        Session["Nombre"] = Convert.ToString(oResult["NOMBRES"]) + Convert.ToString(oResult["APELLIDOS"]);
                        Session["idccms"] = Convert.ToInt32(oResult["CCMS"]);
                        Session["createUser"] = Convert.ToInt32(oResult["createUser"]);
                        Session["editPermissions"] = Convert.ToInt32(oResult["editPermissions"]);
                        Session["AddCampaigns"] = Convert.ToInt32(oResult["AddCampaigns"]);
                        Session["myTeams"] = Convert.ToInt32(oResult["myTeams"]);
                        Session["randomnessResults"] = Convert.ToInt32(oResult["randomnessResults"]);
                        Session["random"] = Convert.ToInt32(oResult["random"]);
                        Session["planCalculator"] = Convert.ToInt32(oResult["planCalculator"]);
                        Session["daysCalculator"] = Convert.ToInt32(oResult["daysCalculator"]);
                        Session["bitacoraSura"] = Convert.ToInt32(oResult["bitacoraSura"]);
                        Session["Estatus"] = "Activo";

                        try
                        {
                            Session["LastLogin"] = Convert.ToString(Convert.ToDateTime(oResult["LAST_LOGIN"]).ToString("dddd, dd MMM yy | HH:mm:ss "));
                        }
                        catch (Exception)
                        {
                            Session["LastLogin"] = DateTime.Now.ToString("dddd, dd MMM yy | HH:mm:ss ");
                        }

                        updateLastLoginByUser.Process();


                        Connection.oConnectionQaWeb.Close();
                        return 1;
                    }
                    else
                    {

                        Connection.oConnectionQaWeb.Close();
                        return 0;

                    }
                }
                else
                {

                    Connection.oConnectionQaWeb.Close();
                    return 0;
                }


            }
            catch (Exception)
            {
                return 0;
            }

        }

    }
}