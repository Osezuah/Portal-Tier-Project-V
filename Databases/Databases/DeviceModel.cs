using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    [PrimaryKey("Id")]
    public class DeviceModel
    {
        //id is device id 
        public int Id { get; set; }

        // name is device name 
        public string Name { get; set; }

        //state in this the Active attribute in the Device table from database model which represents active/not active, It would be a true if active or false if inactive ( boolean )
        public bool State { get; set; }

        public DateTime LastUpdated { get; set; }

        //Device Activity is same as Device Logs which we previously used..
        public DeviceLogModel DeviceActivity { get; set; }
    }
}
