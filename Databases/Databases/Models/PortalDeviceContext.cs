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
        public DbSet<Device> devices { get; set; }
        public DbSet<DeviceGroup> devicegroups { get; set; }
        public DbSet<DeviceLog> devicelogs { get; set; }
        public DbSet<DeviceType> deviceTypes { get; set; }
        public DbSet<Home> homes { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // relationships
            modelBuilder.Entity<DeviceLog>().HasOne(DeviceLog => DeviceLog.Device)
                .WithMany(Device => Device.Logs)
                .HasForeignKey(DeviceLog => DeviceLog.DeviceId);

            modelBuilder.Entity<User>()
                .HasMany<Home>(User => User.Homes)
                .WithOne(Home => Home.User)
                .HasForeignKey(Home => Home.UserId);

            modelBuilder.Entity<Home>()
                .HasMany(e => e.Rooms)
                .WithOne(e => e.ParentHome)
                .HasForeignKey("HomeId");

            modelBuilder.Entity<Room>()
                .HasMany<Device>(Room => Room.Devices)
                .WithOne(Device => Device.ParentRoom)
                .HasForeignKey("RoomId")
                .IsRequired(false);
            
            modelBuilder.Entity<DeviceGroup>()
                .HasMany<Device>(Room => Room.Devices)
                .WithOne(Device => Device.ParentGroup)
                .HasForeignKey("GroupId")
                .IsRequired(false);
            
            modelBuilder.Entity<DeviceType>()
                .HasMany<Device>(DeviceType => DeviceType.Devices)
                .WithOne(Device => Device.ParentType)
                .HasForeignKey("TypeId")
                .IsRequired(false);
        }
    }
}
