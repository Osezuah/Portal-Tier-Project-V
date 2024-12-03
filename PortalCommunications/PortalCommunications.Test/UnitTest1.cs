namespace PortalCommunications.Tests
{
    // Official code coverage report located in "coverageReport" directory, open index.html
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using PortalCommunications;
    using PortalCommunications.Models;
    using Xunit;

    public class PortalCADInterfaceTests
    {
        private PortalDeviceContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<PortalDeviceContext>()
                          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                          .Options;
            return new PortalDeviceContext(options);
        }

        [Fact]
        public void RegisterNewUser_ShouldAddUserToDatabase()
        {
            using var context = GetInMemoryContext();
            var interfaceObj = new PortalCADInterface(context);

            var newUser = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            };

            var result = interfaceObj.RegisterNewUser(newUser);

            Assert.True(result);
            Assert.Single(context.users);
            Assert.Equal("John", context.users.First().FirstName);
        }

        [Fact]
        public void AddDevice_ShouldAddDeviceToDatabase()
        {
            using var context = GetInMemoryContext();

            var device = new Device
            {
                Id = 1,
                Name = "TestDevice",
                TypeId = 1,
                RoomId = 1,
                GroupId = 1,
                State = "Active"
            };

            context.devices.Add(device);
            context.SaveChanges();

            Assert.Single(context.devices);
            Assert.Equal("TestDevice", context.devices.First().Name);
        }

        [Fact]
        public void AddDeviceGroup_ShouldAddGroupToDatabase()
        {
            using var context = GetInMemoryContext();

            var group = new DeviceGroup
            {
                Id = 1,
                Name = "TestGroup"
            };

            context.devicegroups.Add(group);
            context.SaveChanges();

            Assert.Single(context.devicegroups);
            Assert.Equal("TestGroup", context.devicegroups.First().Name);
        }

        [Fact]
        public void AddDeviceLog_ShouldAddLogToDatabase()
        {
            using var context = GetInMemoryContext();

            var log = new DeviceLog
            {
                Id = 1,
                DeviceId = 1,
                LoggedState = "On",
                LoggedTime = DateTime.UtcNow
            };

            context.devicelogs.Add(log);
            context.SaveChanges();

            Assert.Single(context.devicelogs);
            Assert.Equal("On", context.devicelogs.First().LoggedState);
        }

        [Fact]
        public void AddDeviceType_ShouldAddTypeToDatabase()
        {
            using var context = GetInMemoryContext();

            var deviceType = new DeviceType
            {
                Id = 1,
                Name = "Light"
            };

            context.deviceTypes.Add(deviceType);
            context.SaveChanges();

            Assert.Single(context.deviceTypes);
            Assert.Equal("Light", context.deviceTypes.First().Name);
        }

        [Fact]
        public void AddHome_ShouldAddHomeToDatabase()
        {
            using var context = GetInMemoryContext();

            var home = new Home
            {
                Id = 1,
                Name = "TestHome",
                UserId = 1
            };

            context.homes.Add(home);
            context.SaveChanges();

            Assert.Single(context.homes);
            Assert.Equal("TestHome", context.homes.First().Name);
        }

        [Fact]
        public void AddRoom_ShouldAddRoomToDatabase()
        {
            using var context = GetInMemoryContext();

            var room = new Room
            {
                Id = 1,
                Name = "Living Room",
                HomeId = 1
            };

            context.rooms.Add(room);
            context.SaveChanges();

            Assert.Single(context.rooms);
            Assert.Equal("Living Room", context.rooms.First().Name);
        }

        [Fact]
        public void AddUser_ShouldAddUserToDatabase()
        {
            using var context = GetInMemoryContext();

            var user = new User
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice@example.com",
                Password = "password123"
            };

            context.users.Add(user);
            context.SaveChanges();

            Assert.Single(context.users);
            Assert.Equal("Alice", context.users.First().FirstName);
        }

        [Fact]
        public void GetDeviceById_ShouldReturnCorrectDevice()
        {
            using var context = GetInMemoryContext();
            var interfaceObj = new PortalCADInterface(context);

            var device = new Device
            {
                Id = 1,
                Name = "Device1",
                TypeId = 1,
                RoomId = 1,
                GroupId = 1,
                State = "Active"
            };

            context.devices.Add(device);
            context.SaveChanges();

            var result = interfaceObj.GetDeviceById(1);

            Assert.NotNull(result);
            Assert.Equal("Device1", result.Name);
        }

        [Fact]
        public void UpdateChangesInDatabase_ShouldUpdateDeviceDetails()
        {
            using var context = GetInMemoryContext();
            var interfaceObj = new PortalCADInterface(context);

            var device = new Device
            {
                Id = 1,
                Name = "OldDevice",
                TypeId = 1,
                RoomId = 1,
                GroupId = 1,
                State = "Inactive"
            };

            context.devices.Add(device);
            context.SaveChanges();

            var updatedDevice = new Device
            {
                Id = 1,
                Name = "UpdatedDevice",
                TypeId = 2,
                RoomId = 2,
                GroupId = 2,
                State = "Active"
            };

            var result = interfaceObj.UpdateChangesInDatabase(updatedDevice);

            Assert.True(result);
            Assert.Equal("UpdatedDevice", context.devices.First().Name);
            Assert.Equal(2, context.devices.First().TypeId);
        }

        [Fact]
        public void Room_Delete_ShouldReturnFalseIfRoomNotFound()
        {
            using var context = GetInMemoryContext();
            var interfaceObj = new PortalCADInterface(context);

            var result = interfaceObj.Room_Delete();

            Assert.False(result);
        }
    }
}
