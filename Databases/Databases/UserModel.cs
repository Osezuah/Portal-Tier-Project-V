using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    [PrimaryKey("Id")]
    public class UserModel
    {
        public int Id { get; set; } //primary key
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
