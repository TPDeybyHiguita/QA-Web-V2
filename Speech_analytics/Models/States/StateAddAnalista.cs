using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Speech_analytics.Models.States
{
    public class StateAddAnalista
    {
        public bool StateExistUser { get; set; }
        public bool StateSaveManagerAnalyst { get; set; }
        public bool StateSaveManagerPermissions { get; set; }
        public bool StateUpdateStateManagerAnalys { get; set; }
    }
}