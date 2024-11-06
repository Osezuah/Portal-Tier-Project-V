using Microsoft.EntityFrameworkCore;
using PortalCommunicationsAPI.Models;

namespace PortalCommunicationsAPI.Data
{
    public class PortalCommunicationsDbContext : DbContext
    {
        public DbSet<Device> devices { get; set; }
        public DbSet<Thermostat> thermostats { get; set; }
        public DbSet<SmartFridge> smartFridges { get; set; }
        public DbSet<SmartVacuum> smartVacuums { get; set; }
        public DbSet<Dehumidifier> dehumidifiers { get; set; }
        public DbSet<SmartOven> smartOvens { get; set; }
        public DbSet<DeviceLog> devicesLog { get; set; }

        public PortalCommunicationsDbContext(DbContextOptions<PortalCommunicationsDbContext> options) : base(options)
        {
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

            //// relationships
            //modelBuilder.Entity<DeviceLog>().HasOne(log => log.device)
            //    .WithMany(device => device.DeviceLogs)
            //    .HasForeignKey(log => log.deviceId);
        }

        public DbSet<DeviceLog> DeviceLogs { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Thermostat> Thermostats { get; set; }
        public DbSet<SmartFridge> SmartFridges { get; set; }
        public DbSet<SmartVacuum> SmartVacuums { get; set; }
        public DbSet<Dehumidifier> Dehumidifiers { get; set; }
        public DbSet<SmartOven> SmartOvens { get; set; }

    }

}
