using Microsoft.EntityFrameworkCore;
using PortalCommunicationsAPI.Models;

namespace PortalCommunicationsAPI.Data
{
    public class PortalCommunicationsDbContext : DbContext
    {
        public PortalCommunicationsDbContext(DbContextOptions<PortalCommunicationsDbContext> options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }

        // Define DbSets for each entity/table, e.g., Users
        public DbSet<User> Users { get; set; }

        //seed devices
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().HasData(
                new Device
                {
                    Id = 1,
                    Name = "Alarm",
                    Status = "Active"
                },
                new Device
                {
                    Id = 2,
                    Name = "Soundbar",
                    Status = "Inactive"
                },
                new Device
                {
                    Id = 3,
                    Name = "Bedroom Light 1",
                    Status = "Active"
                },
                new Device
                {
                    Id = 4,
                    Name = "Bedroom Light 2",
                    Status = "Active"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User {
                    UserId = 1,
                    Username = "admin",
                    Password = "admin"
                },
                new User
                {
                    UserId = 2,
                    Username = "user",
                    Password = "user"
                },
                new User
                {
                    UserId = 3,
                    Username = "user2",
                    Password = "user2"
                }
                );

        }


    }

}
