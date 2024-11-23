using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Models
{
    public class DeviceType : PortalDeviceContext
    {
        [Required]
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public ICollection<Device> Devices { get; set; }
    }
}