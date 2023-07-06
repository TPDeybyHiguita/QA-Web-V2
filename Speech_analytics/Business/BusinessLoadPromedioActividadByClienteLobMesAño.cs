using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Business
{
    public class BusinessLoadPromedioActividadByClienteLobMesAño
    {
        private MANAGER_CONFIGURATION managerSentting;
        private int promedioActividad;

        public BusinessLoadPromedioActividadByClienteLobMesAño(MANAGER_CONFIGURATION managerSentting)
        {
            this.managerSentting = managerSentting;
            promedioActividad = 0;
        }

        public int Process()
        {
            LoadDataManagerSetting loadDataManagerSetting = new LoadDataManagerSetting(managerSentting);
            managerSentting = loadDataManagerSetting.Process();
            promedioActividad = Convert.ToInt32(managerSentting.PromedioActividad);
            return promedioActividad;
        }
    }
}