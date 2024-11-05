using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Models
{
    public class PortalDeviceContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // relationships
            modelBuilder.Entity<DeviceLog>().HasOne(log => log.device)
                .WithMany(device => device.DeviceLogs)
                .HasForeignKey(log => log.deviceId);

            // User one-to-many relationship wherein a User may have
            // multiple homes
            modelBuilder.Entity<User>()
                .HasMany<Home>(User => User.Homes)
                .WithOne(Home => Home.User)
                .HasForeignKey(Home => Home.UserId)
                .IsRequired(true);


        }
    }
}
