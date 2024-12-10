using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalCommunications
{
    public class DeviceLog
    {
        [Required]
        [Key]
        public int Id { get; set; } // PK

        [Required]
        public int DeviceId { get; set; } // FK

        [Required]
        public string LoggedState { get; set; } = string.Empty;

        [Required]
        public DateTime LoggedTime { get; set; } // time of when log was created

        public Device Device { get; set; } = null!;// navigation property
    }
}
