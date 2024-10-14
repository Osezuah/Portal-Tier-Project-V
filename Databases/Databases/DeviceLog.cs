using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    public class DeviceLog
    {
        public int deviceLogId { get; set; } // PK
        public int deviceId { get; set; } // FK
        public string loggedMessage { get; set; }
        public DateTime loggedTime { get; set; } // time of when log was created

        public Device device { get; set; } // navigation property
    }
}
