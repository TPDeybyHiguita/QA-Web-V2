using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class USERDATA
    {
        public int CCMS { get; set; }
        public string USER { get; set; }
        public int CCMS_MANAGER { get; set; }
        public int CCMSMANAGER { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string EMAIL { get; set; }
        public string EMAIL_ALTERNO { get; set; }
        public string Estado { get; set; }
        public string LAST_LOGIN { get; set; }
        public SessionModel SessionModel { get; set; }
        public PERMISOS_USERS PERMISOS_USERS { get; set; }
        public USER_LOGIN USER_LOGIN { get; set; }
    }
}