using Speech_analytics.Data;
using Speech_analytics.Models;
using Speech_analytics.Models.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Business
{
    public class BusinessSenttingManager
    {
        private MANAGER_CONFIGURATION managerSentting;
        private HomologacionSkil homologacionSkil;
        private StateBusinessSenttingManager stateBusinessSenttingManager;

        public BusinessSenttingManager(MANAGER_CONFIGURATION managerSentting, string USER_RED_MANAGER)
        {
            this.managerSentting = managerSentting;
            stateBusinessSenttingManager = new StateBusinessSenttingManager();
            homologacionSkil= new HomologacionSkil();
            managerSentting.UserRedManager= USER_RED_MANAGER;
        }

        public StateBusinessSenttingManager Process()
        {
            try
            {
                LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(managerSentting.UserRedManager);
                LoadDataInSkillClient loadDataInSkillClient = new LoadDataInSkillClient(Convert.ToString(managerSentting.IdLob), Convert.ToString(managerSentting.Cliente));
                managerSentting.CcmsManager = Convert.ToString(loadCcmsInDataUserByUserRed.Process());
                homologacionSkil = loadDataInSkillClient.Process();
                managerSentting.Lob = homologacionSkil.Nombre;
                VerifyExistSenttingManagerByMesAñoIdLobInManagerConfiguration verifyExistSenttingManagerByMesAñoIdLobInManagerConfiguration = new VerifyExistSenttingManagerByMesAñoIdLobInManagerConfiguration(managerSentting);
                SaveSenttingManagerInManagerCondiguration saveSenttingManagerInManagerCondiguration = new SaveSenttingManagerInManagerCondiguration(managerSentting);
                UpdateSenttingManagerInManagerConfiguration updateSenttingManagerInManagerConfiguration = new UpdateSenttingManagerInManagerConfiguration(managerSentting);
                stateBusinessSenttingManager.StateVeryExistSenttingManager = verifyExistSenttingManagerByMesAñoIdLobInManagerConfiguration.Process();


                if (stateBusinessSenttingManager.StateVeryExistSenttingManager == false)
                {
                    stateBusinessSenttingManager.StateSaveSenttingManager = saveSenttingManagerInManagerCondiguration.Process();
                }
                else if (stateBusinessSenttingManager.StateVeryExistSenttingManager == true)
                {
                    stateBusinessSenttingManager.StateUpdateSenttingManager = updateSenttingManagerInManagerConfiguration.Process();
                }
                else
                {
                    stateBusinessSenttingManager.StateSaveSenttingManager = false;
                    stateBusinessSenttingManager.StateUpdateSenttingManager = false;
                }

                return stateBusinessSenttingManager;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





    }
}