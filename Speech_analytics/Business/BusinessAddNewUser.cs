using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Speech_analytics.Clases;
using Speech_analytics.Data;

namespace Speech_analytics.Business
{
    public class BusinessAddNewUser
    {
        private readonly USERDATA userData;
        private StateAddNewUsers stateAddNewUsersResponse;

        public BusinessAddNewUser(USERDATA userData) 
        {
            this.userData = userData;
            stateAddNewUsersResponse = new StateAddNewUsers();
        }
        public StateAddNewUsers Process()
        {
            try
            {

                verifyUserExistByCcms verifyUserExistByCcms = new verifyUserExistByCcms(userData);
                saveDataUserPermissions saveDataUserPermissions = new saveDataUserPermissions(userData);
                saveUserData saveUserData = new saveUserData(userData);
                SaveDataUserLogin saveDataUserLogin = new SaveDataUserLogin(userData);
                stateAddNewUsersResponse.StateExistUser = verifyUserExistByCcms.Process();

                if (stateAddNewUsersResponse.StateExistUser == false)
                {
                    stateAddNewUsersResponse.StateUserData = saveUserData.Process();
                    stateAddNewUsersResponse.StateUserLogin = saveDataUserLogin.Process();
                    stateAddNewUsersResponse.StateUserPermissions = saveDataUserPermissions.Process();
                }
                else
                {
                    stateAddNewUsersResponse.StateUserData = false;
                    stateAddNewUsersResponse.StateUserLogin = false;
                    stateAddNewUsersResponse.StateUserPermissions = false;
                }

                return stateAddNewUsersResponse;

            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }
    }
}