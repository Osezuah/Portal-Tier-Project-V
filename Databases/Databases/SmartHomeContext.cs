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
        public DbSet<Device> devices { get; set; }
        public DbSet<Thermostat> thermostats { get; set; }
        public DbSet<SmartFridge> smartFridges { get; set; }
        public DbSet<SmartVacuum> smartVacuums { get; set; }
        public DbSet<Dehumidifier> dehumidifiers { get; set; }
        public DbSet<SmartOven> smartOvens { get; set; }
        public DbSet<DeviceLog> devicesLog { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            // Setup Sql server here
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setup device inheritance
            modelBuilder.Entity<Device>()
                .HasDiscriminator<string>("DeviceType")
                .HasValue<Thermostat>("Thermostat")
                .HasValue<SmartFridge>("SmartFridge")
                .HasValue<SmartVacuum>("SmartVacuum")
                .HasValue<Dehumidifier>("Dehumidifier")
                .HasValue<SmartOven>("SmartOven");

            // relationships
            modelBuilder.Entity<DeviceLog>().HasOne(log => log.device)
                .WithMany(device => device.DeviceLogs)
                .HasForeignKey(log => log.deviceId);
        }

    }
}
