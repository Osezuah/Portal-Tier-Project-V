using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PortalCommunications.Models
{
    [PrimaryKey(nameof(Id))]
    public class Room
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Home")]
        public int HomeId { get; set; }

        public Home ParentHome { get; set; }
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}

