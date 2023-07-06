using Speech_analytics.Data;
using Speech_analytics.Models;
using Speech_analytics.Models.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Business
{
    public class BusinessAddAnyst
    {
        private MANAGER_ANALISTA managerAnalista;
        private StateAddAnalista stateAddAnalista;

        public BusinessAddAnyst(MANAGER_ANALISTA managerAnalista, string USER_RED_MANAGER)
        {
            this.managerAnalista = managerAnalista;
            stateAddAnalista = new StateAddAnalista();
            managerAnalista.USER_RED_MANAGER= USER_RED_MANAGER;
        }

        public StateAddAnalista Process()
        {
            try
            {
                LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRedAnalista = new LoadCcmsInDataUserByUserRed(managerAnalista.USER_RED);
                LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRedManager = new LoadCcmsInDataUserByUserRed(managerAnalista.USER_RED_MANAGER);
                managerAnalista.CCMS_ANALISTA = Convert.ToString(loadCcmsInDataUserByUserRedAnalista.Process());
                managerAnalista.CCMS_MANAGER = Convert.ToString(loadCcmsInDataUserByUserRedManager.Process());

                LoadDataUserByUserRedInManagerAnalista loadDataUserByUserRedInManagerAnalista = new LoadDataUserByUserRedInManagerAnalista(managerAnalista.USER_RED);
                SaveDataInManagerAnalista saveDataInManagerAnalista = new SaveDataInManagerAnalista(managerAnalista);
                SaveDataInManagerPermisos saveDataInManagerPermisos = new SaveDataInManagerPermisos(managerAnalista);
                UpdateEstadoInManagerAnalista updateEstadoInManagerAnalista = new UpdateEstadoInManagerAnalista(managerAnalista);


                stateAddAnalista.StateExistUser = loadDataUserByUserRedInManagerAnalista.Process();

                if (stateAddAnalista.StateExistUser == false)
                {
                    stateAddAnalista.StateSaveManagerAnalyst = saveDataInManagerAnalista.Process();
                    stateAddAnalista.StateSaveManagerPermissions = saveDataInManagerPermisos.Process();
                }
                else if (stateAddAnalista.StateExistUser == true)
                {
                    stateAddAnalista.StateUpdateStateManagerAnalys = updateEstadoInManagerAnalista.Process();
                }
                else
                {
                    stateAddAnalista.StateSaveManagerAnalyst = false;
                    stateAddAnalista.StateSaveManagerPermissions = false;
                    stateAddAnalista.StateUpdateStateManagerAnalys = false;
                }

                return stateAddAnalista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }

     

     

}