using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    [PrimaryKey("deviceLogId")]
    public class DeviceLogModel
    {
        public int deviceLogId { get; set; } // PK

        [ForeignKey("deviceId")]
        [InverseProperty("DeviceActivity")]
        public int deviceId { get; set; } // FK

        //Logged message is the attribute of type string which contains the unique attributes of each specific device class in a CSV format
        public string loggedMessage { get; set; }

        public DateTime loggedTime { get; set; }
    }
}
