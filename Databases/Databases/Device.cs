using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    public class Device
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }
        public string State { get; set; }

        public ICollection<DeviceLog> DeviceLogs { get; set; } // Logging data for the device
        public DateTime LastUpdated { get; set; } // Last time the status was updated

    }
}
