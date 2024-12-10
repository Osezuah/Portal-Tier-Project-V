using PortalCommunications.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PortalCommunications
{
    public class Device
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int TypeId { get; set; }

        [DefaultValue(null)]
        public string State { get; set; } = string.Empty;

        [DefaultValue(null)]
        public int RoomId { get; set; }

        [DefaultValue(null)]
        public int GroupId { get; set; }

        public DeviceType ParentType { get; set; } = null!;
        public ICollection<DeviceLog> Logs { get; set; } = null!; // Logging data for the device
        public Room? ParentRoom { get; set; } = null!;
        public DeviceGroup? ParentGroup { get; set; } = null!;
        public DateTime LastUpdated { get; set; }// Last time the status was updated
    }
}
