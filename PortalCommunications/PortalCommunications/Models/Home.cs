using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalCommunications.Models
{
    public class Home
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}