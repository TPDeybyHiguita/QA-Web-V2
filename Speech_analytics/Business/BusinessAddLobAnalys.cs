using Speech_analytics.Data;
using Speech_analytics.Models;
using Speech_analytics.Models.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Business
{
    public class BusinessAddLobAnalys
    {
        private MANAGER_ANALISTA managerAnalista;
        private USERDATA userData;
        private StateAddLob stateAddLob;

        public BusinessAddLobAnalys (MANAGER_ANALISTA managerAnalista, string UserRedManager)
        {
            this.managerAnalista = managerAnalista;
            this.managerAnalista.USER_RED_MANAGER= UserRedManager;
            stateAddLob = new StateAddLob();
            userData= new USERDATA();
        }

        public StateAddLob Process()
        {
            try
            {
                LoadUserRedByCcmsInUserData loadUserRedByCcmsInUserData = new LoadUserRedByCcmsInUserData(Convert.ToInt32(managerAnalista.CCMS_ANALISTA));
                LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(managerAnalista.USER_RED_MANAGER);
                managerAnalista.USER_RED = loadUserRedByCcmsInUserData.Process();
                managerAnalista.CCMS_MANAGER = Convert.ToString(loadCcmsInDataUserByUserRed.Process());
                VerifyDataInManagerPermisos verifyDataInManagerPermisos = new VerifyDataInManagerPermisos(managerAnalista);
                SaveDataInManagerPermisos saveDataInManagerPermisos = new SaveDataInManagerPermisos(managerAnalista);
                                
                stateAddLob.StateExisManagerPermisos = verifyDataInManagerPermisos.Process();

                if (stateAddLob.StateExisManagerPermisos == false)
                {
                    stateAddLob.StateSaveLobAnalys = saveDataInManagerPermisos.Process();
                }
                else
                {
                    stateAddLob.StateSaveLobAnalys = false;
                }

                return stateAddLob;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}