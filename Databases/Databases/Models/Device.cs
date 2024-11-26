using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Databases.Models;

namespace Databases
{
    public class Device : PortalDeviceContext
    {
        [Required]
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
        
        [DefaultValue(null)]
        public string State { get; set; }
        
        [DefaultValue(null)]
        public int RoomId { get; set; }
        
        [DefaultValue(null)]
        public int GroupId { get; set; }
        
        public DeviceType ParentType { get; set; }
        public ICollection<DeviceLog> Logs { get; set; } // Logging data for the device
        public Room? ParentRoom { get; set; }
        public DeviceGroup? ParentGroup { get; set; }
        public DateTime LastUpdated { get; set; } // Last time the status was updated
    }
}
