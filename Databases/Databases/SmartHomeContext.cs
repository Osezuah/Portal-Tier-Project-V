using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    public class SmartHomeContext : DbContext
    {
        public DbSet<DeviceModel> devices { get; set; }
        public DbSet<DeviceLogModel> devicesLog { get; set; }
        public DbSet<UserModel> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(@"Data Source=C:\\Temp\Demo.db");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
