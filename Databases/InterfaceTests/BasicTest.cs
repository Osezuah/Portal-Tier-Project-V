using Xunit;
using Databases;
namespace Databases.Tests
{
    // The testing works when Solution file is set to a "Class Library" and not "Console Application" because Program.cs doesn't have a main function yet.
    public class PortalCADInterfaceTests
    {
        private readonly PortalCADInterface _portalCADInterface;

        public PortalCADInterfaceTests()
        {
            _portalCADInterface = new PortalCADInterface();
        }
        #region User Testing
        [Fact]
        public void UserLoginReturnMinusOneSuccessful()
        {
            int result = _portalCADInterface.User_Login("wrong@email.com", "wrongpass");

            Assert.Equal(-1, result);
        }
        #endregion

        #region Home Testing
        [Fact]
        public void DeleteHomeReturnsFalseSuccessful()
        {
            bool result = _portalCADInterface.Home_Delete("Home");
            Assert.False(result);
        }
        #endregion

        #region Room Testing
        [Fact]
        public void RoomDeleteReturnsFalseSuccessful()
        {
            bool result = _portalCADInterface.Room_Delete();
            Assert.False(result);
        }
        #endregion

        #region Device Testing
        [Fact]
        public void DeviceDeleteReturnFalseSuccessful()
        {
            bool result = _portalCADInterface.Device_Delete("DeviceExample");

            Assert.False(result);
        }
        #endregion

        #region DeviceType Testings
        [Fact]
        public void DeviceTypeDeleteReturnFalseSuccessful()
        {
            bool result = _portalCADInterface.DeviceType_Delete("DeviceTypeExample");
        }
        #endregion

        #region DeviceGroup Testing
        [Fact]
        public void DeviceGroupDeleteReturnFalseSuccessful()
        {
            bool result = _portalCADInterface.DeviceGroup_Delete("DeviceGroupExample");
            Assert.False(result);
        }
        #endregion
    }
}