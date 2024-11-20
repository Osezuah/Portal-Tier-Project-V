using PortalCommunications.Components.Device_Class;
using NUnit.Framework;
using System;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            // Initialize any resources needed for tests here
        }


        [Test]
        public void Validate_InvalidName_ReturnsFalse()
        {
            // Arrange
            var lockDevice = new Lock(1, "Invalid lock name", true, DateTime.Now, 1001, "true");

            // Act
            var result = lockDevice.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_InvalidState_ReturnsFalse()
        {
            // Arrange
            var lockDevice = new Lock(1, "Front door smart lock", true, DateTime.Now, 1001, "invalid");

            // Act
            var result = lockDevice.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_VeryLongName_ReturnsFalse()
        {
            // Arrange
            var longName = new string('A', 1000); // Name with 1000 characters
            var lockDevice = new Lock(1, longName, true, DateTime.Now, 1001, "true");

            // Act
            var result = lockDevice.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_NullLoggedMessage_ReturnsFalse()
        {
            // Arrange
            var lockDevice = new Lock(1, "Front door smart lock", true, DateTime.Now, 1001, null);

            // Act
            var result = lockDevice.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_ValidFridgeData_ReturnsTrue()
        {
            // Arrange
            var message = "fridgeTemperature=5,freezerTemperature=-10";
            var fridge = new SmartFridge(1, "Smart Fridge", true, DateTime.Now, 1002, message);

            // Act
            var result = fridge.Validate();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_InvalidFridgeTemperature_ReturnsFalse()
        {
            // Arrange
            var message = "fridgeTemperature=20,freezerTemperature=-10";
            var fridge = new SmartFridge(1, "Smart Fridge", true, DateTime.Now, 1002, message);

            // Act
            var result = fridge.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_InvalidFreezerTemperature_ReturnsFalse()
        {
            // Arrange
            var message = "fridgeTemperature=5,freezerTemperature=-30";
            var fridge = new SmartFridge(1, "Smart Fridge", true, DateTime.Now, 1002, message);

            // Act
            var result = fridge.Validate();

            // Assert
            Assert.IsFalse(result);
        }


        [Test]
        public void UserCreation_ValidData_ReturnsUserObject()
        {
            // Arrange
            var userId = 1;
            var username = "testUser";
            var password = "password123";
            var email = "test@example.com";

            // Act
            var user = new User(userId, username, password, email);

            // Assert
            Assert.AreEqual(userId, user.id);
            Assert.AreEqual(username, user.username);
            Assert.AreEqual(password, user.password);
            Assert.AreEqual(email, user.email);
        }

        [Test]
        public void UserCreation_EmptyEmail_ReturnsUserObject()
        {
            // Arrange
            var userId = 1;
            var username = "testUser";
            var password = "password123";
            var email = "";

            // Act
            var user = new User(userId, username, password, email);

            // Assert
            Assert.AreEqual(userId, user.id);
            Assert.AreEqual(username, user.username);
            Assert.AreEqual(password, user.password);
            Assert.AreEqual(email, user.email);
        }

        [Test]
        public void Validate_ValidSensorData_ReturnsTrue()
        {
            // Arrange
            var sensor = new Sensors(1, "Motion sensor", true, DateTime.Now, 1001, "currentReading=50");

            // Act
            var result = sensor.Validate();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_InvalidLoggedMessage_ReturnsFalse()
        {
            // Arrange
            var sensor = new Sensors(1, "Motion sensor", true, DateTime.Now, 1001, "invalidMessage");

            // Act
            var result = sensor.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_InvalidCurrentReading_ReturnsFalse()
        {
            // Arrange
            var sensor = new Sensors(1, "Motion sensor", true, DateTime.Now, 1001, "currentReading=150");

            // Act
            var result = sensor.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_InvalidSensorName_ReturnsFalse()
        {
            // Arrange
            var sensor = new Sensors(1, "Invalid sensor name", true, DateTime.Now, 1001, "currentReading=50");

            // Act
            var result = sensor.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_ValidEdgeCurrentReading_ReturnsTrue()
        {
            // Arrange
            var sensorLowEdge = new Sensors(1, "Motion sensor", true, DateTime.Now, 1001, "currentReading=0");
            var sensorHighEdge = new Sensors(1, "Motion sensor", true, DateTime.Now, 1001, "currentReading=100");

            // Act
            var resultLowEdge = sensorLowEdge.Validate();
            var resultHighEdge = sensorHighEdge.Validate();

            // Assert
            Assert.IsTrue(resultLowEdge);
            Assert.IsTrue(resultHighEdge);
        }

        [Test]
        public void Validate_ValidThermostatData_ReturnsTrue()
        {

            var message = "ThermostatTemp=25";
            var thermostat = new Thermostat(1, "Thermostat", true, DateTime.Now, 1003, message);

            var result = thermostat.Validate();

            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_TemperatureBelowRange_ReturnsFalse()
        {
            // Arrange
            var message = "ThermostatTemp=-5";
            var thermostat = new Thermostat(1, "Thermostat", true, DateTime.Now, 1003, message);

            // Act
            var result = thermostat.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_TemperatureAboveRange_ReturnsFalse()
        {
            // Arrange
            var message = "ThermostatTemp=60";
            var thermostat = new Thermostat(1, "Thermostat", true, DateTime.Now, 1003, message);

            // Act
            var result = thermostat.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_MissingTemperatureData_ReturnsFalse()
        {
            // Arrange
            var message = "InvalidData";
            var thermostat = new Thermostat(1, "Thermostat", true, DateTime.Now, 1003, message);

            // Act
            var result = thermostat.Validate();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_InvalidTemperatureFormat_ReturnsFalse()
        {
            // Arrange
            var message = "ThermostatTemp=NotANumber";
            var thermostat = new Thermostat(1, "Thermostat", true, DateTime.Now, 1003, message);

            // Act
            var result = thermostat.Validate();

            // Assert
            Assert.IsFalse(result);
        }
    }
}