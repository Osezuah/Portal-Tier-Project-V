using Microsoft.EntityFrameworkCore;

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

            //seed data 
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "John Doe",
                    Email = "JohnDoe@hotmail.com",
                    Password = "password"
                },
                new User
                {
                    Id = 2,
                    Username = "Jane Doe",
                    Email = "JanDoe@hotmail.com",
                    Password = "password"
                },
                new User
                {
                    Id = 3,
                    Username = "John Smith",
                    Email = "JohnSmith@hotmail.com",
                    Password = "password"
                });
        }

        public DbSet<DeviceLog> DeviceLogs { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Thermostat> Thermostats { get; set; }
        public DbSet<SmartFridge> SmartFridges { get; set; }
        public DbSet<SmartVacuum> SmartVacuums { get; set; }
        public DbSet<Dehumidifier> Dehumidifiers { get; set; }
        public DbSet<SmartOven> SmartOvens { get; set; }

        public DbSet<User> Users { get; set; } //work from the business layer


    }

}
