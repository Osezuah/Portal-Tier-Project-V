using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    // This is the class other groups will use to interface with the Portal Command and Data Layer
    public class PortalCADInterface()
    {
        //Interface Hookups for User
        #region User
        public void User_CreateUser(String FirstName, String LastName, String Email, String Password)
        {
            //Add User to Database based on incomming parameters
            //CreateUser(FirstName, LastName, Email, Password);

        }

        public bool User_Login(String Email, String Password)
        {
            bool Result = false;

            // Check if Email and Password Match on file
            // Result = CheckLogin(Email, Password)

            // If there is a match, return true, else, return false
            return Result;
 
        }
        #endregion

        //Interface Hookups for Home
        #region Home
        public void Home_Create()
        {

        }

        public void Home_Edit()
        {

        }

        public void Home_Delete()
        {

        }
        public void Home_FindByName()
        {

        }
        #endregion

        //Interface Hookups for Room
        #region Room
        public void Room_Create()
        {

        }

        public void Room_Edit()
        {

        }

        public void Room_Delete()
        {

        }
        public void Room_FindByName()
        {

        }
        #endregion

        //Interface Hookups for Device
        #region Device
        public void Device_Create()
        {

        }

        public void Device_Edit()
        {

        }

        public void Device_Delete()
        {

        }

        public void Device_FindLastState()
        {

        }

        public void Device_FindListStates()
        {

        }
        #endregion

        //Interface Hookups for DeviceType
        #region DeviceType
        public void DeviceType_Create()
        {

        }

        public void DeviceType_Edit()
        {

        }

        public void DeviceType_Delete()
        {

        }
        public void DeviceType_FindByName()
        {

        }
        #endregion

        //Interface Hookups for DeviceGroup
        #region DeviceGroup
        public void DeviceGroup_Create()
        {

        }

        public void DeviceGroup_Edit()
        {

        }

        public void DeviceGroup_Delete()
        {

        }
        public void DeviceGroup_FindByName()
        {

        }
        #endregion
    }
}
