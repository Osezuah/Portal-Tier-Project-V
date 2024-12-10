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
    [PrimaryKey(nameof(Id))]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [DefaultValue(null)]
        public string? FirstName { get; set; }
        [DefaultValue(null)]
        public string? LastName { get; set; }
        [DefaultValue(null)]
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; } = null!;

        public ICollection<Home> Homes { get; set; } = new List<Home>();

        public static explicit operator User(PortalCommunications.Components.Device_Class.User v)
        {
            throw new NotImplementedException();
        }

/*        public static explicit operator User(PortalCommunications.Components.Device_Class.User v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator User(global::PortalCommunications.Components.Device_Class.User v)
        {
            throw new NotImplementedException();
        }*/
    }
}
