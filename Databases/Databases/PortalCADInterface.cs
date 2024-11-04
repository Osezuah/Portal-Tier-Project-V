using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public int User_Login(String Email, String Password)
        {
            // Default result for login
            int Result = -1;

            // Check if Email and Password Match on file
            // Result = CheckLogin(Email, Password)

            // If there is a match, return the UserID, else, return -1
            return Result;
 
        }
        #endregion

        //Interface Hookups for Home
        #region Home
        public void Home_Create(string name, int userID)
        {
            // Add Home to Database based on incomming paramemters and link it to a userID
            // CreateHome(name, userID);
        }

        public void Home_Edit(string name, string updatedName)
        {
            // Edit a Home name in database based on incoming parameters
            // UpdateHome(name, updatedName);
        }

        public bool Home_Delete(string name)
        {
            // Default Result for Delete
            bool Result = false;
            // Find home by name
            // Home h1 = Home_FindByName(name);
            //if (h1.name == name)
            //{
            //    Result = true;
            //}
            return Result;
        }
        //public Home Home_FindByName(string name)
        //{
        //    Home h1;
        //    // h1 = FindHome(name);
        //    return h1;
        //}
        #endregion

        //Interface Hookups for Room
        #region Room
        public void Room_Create(string name, int houseID)
        {
            // Add Room to Database based on incomming paramemters and link it to a houseID
            // CreateRoom(name, houseID);
        }

        public void Room_Edit(string name, string updatedName)
        {
            // Edit a Room name in database based on incoming parameters
            // UpdateRoom(name, updatedName);
        }

        public bool Room_Delete()
        {
            // Default Result for Delete
            bool Result = false;
            // Find room by name
            // Room r1 = Room_FindByName(name);
            //if (r1.name == name)
            //{
            //    Result = true;
            //}
            return Result;
        }
        //public Room Room_FindByName(string name)
        //{
        //    Room r1;
        //    r1 = FindRoom(name);
        //    return r1;
        //}
        #endregion

        //Interface Hookups for Device
        #region Device
        public void Device_Create(string name, int typeID, int roomID, int groupID)
        {
            // Add Device to Database based on incomming paramemters and link it to a roomID, groupID and typeID
            // CreateDevice(name, typeID, roomID, groupID);
        }

        public void Device_Edit(string name, string updatedName)
        {
            // Edit a Device name in database based on incoming parameters
            // UpdateDevice(name, updatedName);
        }

        public bool Device_Delete(string name)
        {
            // Default Result for Delete
            bool Result = false;
            // Find device by name
            // Device d1 = Device_FindByName(name);
            //if (d1.name == name)
            //{
            //    Result = true;
            //}
            return Result;
        }

        //public Device Device_FindByName(string name)
        //{
        //    Device d1;
        //    d1 = FindDevice(name);
        //    return d1;
        //}
        //public int Device_FindLastState(name)
        //{
        //    int state = -1;
        //    state = FindDeviceState(name);
        //    return state;
        //}

        //public int[] Device_FindListStates()
        //{
        //    int[] states;
        //    states = FindDeviceStatesHistory(name);
        //    return states;
        //}
        #endregion

        //Interface Hookups for DeviceType
        #region DeviceType
        public void DeviceType_Create(string name)
        {
            // Add DeviceType to Database based on incomming paramemters
            // CreateDeviceType(name);
        }

        public void DeviceType_Edit(string name, string updatedName)
        {
            // Edit a DeviceType name in database based on incoming parameters
            // UpdateDeviceType(name, updatedName);
        }

        public bool DeviceType_Delete(string name)
        {
            // Default Result for Delete
            bool Result = false;
            // Find DeviceType by name
            // DeviceType dt1 = DeviceType_FindByName(name);
            //if (dt1.name == name)
            //{
            //    Result = true;
            //}
            return Result;
        }
        //public DeviceType DeviceType_FindByName(string name)
        //{
        //    DeviceType dt1;
        //    dt1 = FindDeviceType(name);
        //    return dt1;
        //}
        #endregion

        //Interface Hookups for DeviceGroup
        #region DeviceGroup
        public void DeviceGroup_Create(string name)
        {
            // Add DeviceGroup to Database based on incomming paramemters
            // CreateRoom(name);
        }

        public void DeviceGroup_Edit(string name, string updatedName)
        {
            // Edit a DeviceGroup name in database based on incoming parameters
            // UpdateDeviceGroup(name, updatedName);
        }

        public bool DeviceGroup_Delete(string name)
        {
            // Default Result for Delete
            bool Result = false;
            // Find devicegroup by name
            // DeviceGroup dg1 = DeviceGroup_FindByName(name);
            //if (dg1.name == name)
            //{
            //    Result = true;
            //}
            return Result;
        }
        //public DeviceGroup DeviceGroup_FindByName(string name)
        //{
        //    DeviceGroup dg1;
        //    dg1 = FindDeviceGroup(name);
        //    return dg1;
        //}
        #endregion
    }
}
