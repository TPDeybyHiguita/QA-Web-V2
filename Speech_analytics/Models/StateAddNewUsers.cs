using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models
{
    public class StateAddNewUsers
    {
        public bool StateExistUser { get; set; }
        public bool StateUserPermissions { get; set; }
        public bool StateUserData { get; set; }
        public bool StateUserLogin { get; set; }
    }
}