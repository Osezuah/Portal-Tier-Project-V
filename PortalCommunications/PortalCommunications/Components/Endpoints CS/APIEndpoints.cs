using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text.Json;
using PortalCommunications.Components.Device_Class;
using PortalCommunications.Components.Pages;
using Microsoft.AspNetCore.Mvc;
using Databases;
using Databases.Models;

namespace APISeperateFiles;

public class APIEndpoints
{
    public static void Map(WebApplication app)
    {
        ////route that accepts changes made to devices on Home
        app.MapGet("/api/device-status/", async ([FromBody] JsonDocument JSobject) =>
        {
            
    
            string device_name = JSobject.RootElement.GetProperty("name").ToString();
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // This ensures case-insensitive matching
            };

            if (device_name.Contains("Lock"))
            {
                Lock lock_ = JsonSerializer.Deserialize<Lock>(JSobject.RootElement.GetRawText());
                if (lock_.Validate())
                {
                    PortalCommunications.Components.Device_Class.Device device = JsonSerializer.Deserialize<PortalCommunications.Components.Device_Class.Device>(JSobject.RootElement.GetRawText());
                    //UpdateChangesInDatabase(device);
                
                }

            }
            else if(device_name.Contains("Sensor"))
            {
                Sensors sensor = JsonSerializer.Deserialize<Sensors>(JSobject.RootElement.GetRawText());
                if (sensor.Validate())
                {
                    PortalCommunications.Components.Device_Class.Device device = JsonSerializer.Deserialize<PortalCommunications.Components.Device_Class.Device>(JSobject.RootElement.GetRawText());
                    //UpdateChangesInDatabase(device);
                }
            }
            else if (device_name.Contains("Camera"))
            {
                PortalCommunications.Components.Device_Class.Device device = JsonSerializer.Deserialize<PortalCommunications.Components.Device_Class.Device>(JSobject.RootElement.GetRawText());
                //UpdateChangesInDatabase(device);
            }
            else if (device_name.Contains("Alarm"))
            {
                Alarm alarm = JsonSerializer.Deserialize<Alarm>(JSobject.RootElement.GetRawText());
                if (alarm.Validate())
                {
                    PortalCommunications.Components.Device_Class.Device device = JsonSerializer.Deserialize<PortalCommunications.Components.Device_Class.Device>(JSobject.RootElement.GetRawText());
                    //UpdateChangesInDatabase(device);
                }
            }
            else if (device_name.Contains("Tracker"))
            {
                Tracker tracker = JsonSerializer.Deserialize<Tracker>(JSobject.RootElement.GetRawText());
                if (tracker.Validate())
                {
                    PortalCommunications.Components.Device_Class.Device device = JsonSerializer.Deserialize<PortalCommunications.Components.Device_Class.Device>(JSobject.RootElement.GetRawText());
                    //UpdateChangesInDatabase(device);
                }
            }
            else if (device_name.Contains("Fridge"))
            {
                SmartFridge smartFridge = JsonSerializer.Deserialize<SmartFridge>(JSobject.RootElement.GetRawText());
                if (smartFridge.Validate())
                {
                    PortalCommunications.Components.Device_Class.Device device = JsonSerializer.Deserialize<PortalCommunications.Components.Device_Class.Device>(JSobject.RootElement.GetRawText());
                    //UpdateChangesInDatabase(device);
                }
            }
            else if (device_name.Contains("Dehumidifier"))
            {
                PortalCommunications.Components.Device_Class.Device device = JsonSerializer.Deserialize<PortalCommunications.Components.Device_Class.Device>(JSobject.RootElement.GetRawText());
                //UpdateChangesInDatabase(device);
            }
            else if (device_name.Contains("Thermostat"))
            {
                PortalCommunications.Components.Device_Class.Device device = JsonSerializer.Deserialize<PortalCommunications.Components.Device_Class.Device>(JSobject.RootElement.GetRawText());
                //UpdateChangesInDatabase(device);
            }


            // This functions will make a new record in "Device Activity" table with new updated details. This "newDeviceObjectWithUpdatedDetails" object contains the update/new details. This function is called whenever a change is made by Home or Portal UI to any of the devices, and this would return a "true" if the changes are made successfully.
            // UpdateChangesInDatabase(Device newDeviceObjectWithUpdatedDetails);


        });

        //This will be used to get a specific device
        app.MapGet("/api/device/{int Idnumber}", async (Idnumber) =>
        {
            Device deviceData = GetDeviceById(Idnumber);

            if (deviceData == null)
            {
                return Results.NotFound();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var jsonResponse = JsonSerializer.Serialize(deviceData, options);

        });


        ////Create a route that will authenticate user login credentials - Go to line 33 in Login.razor to chaange the formaction link Thomas
        app.MapPost("/api/user", async (User? user) =>
        {
            //check if they exist in the database and return User.username
            if(user == null) {
                return Results.BadRequest(new { Message = "Invalid user data. User cannot be null." });
            }

            //simulated getting from db
            bool userExists = true;
            if (userExists){
                return Results.Ok(new { Id = user.id, Username = user.username, Password = user.password });
            }
            else{
                return Results.NotFound(new { Message = "User not found in the database" });
            }
           
            /*            User user = JsonSerializer.Deserialize<User>(JSobject.RootElement.GetRawText());


                        var json = @"
                        {
                            ""message"": ""User Authenticated""
                        }";

                        var jsonDocumenttosend = JsonDocument.Parse(json);
                        //This function will check if a user exists in the database and return a "true" or "false". The user object passed to the function contains username, password and email. (See the User class)
                        if (DoesUserExists(user))
                        {
                            SendRequestToHome(jsonDocumenttosend);
                        }
            */
        });

        ////Create a route that sends changes user makes to device in the ui to home application
        app.MapPut("/api/device-changes/", async ([FromBody] JsonElement JSobject) =>
        {

            //This Function will return an object of type "Device" which is intialized with data from latest record of "Device Activity" table. 
            Device device = GetLatestDeviceActivityRecord(id);

            JSobjectToSend = JsonSerializer.Serialize(device);
            SendRequestToHome(JSobjectToSend);

        });

        //Create a route that registers devices
        //Expecting a single device within the passed JSON Element.
        app.MapPost("/api/register-device/", async (JsonDocument payload) =>
        {
            //Extract devicetype from json Document. Json Document is read-only, if write access is needed: change to JsonNode
            JsonElement root = payload.RootElement;
            string deviceType = string.Empty;

            try
            {
                deviceType = root.GetProperty("device-type").ToString();
            }
            catch (KeyNotFoundException)
            {
                return "device-type not found";
                throw;
            }
            
            Console.WriteLine("Identified Device type as: " + deviceType);
            
            //Business Layer Function(Pass RootElement and DeviceType)

            //Return message
            return deviceType;
        });


        ////create a route that registers a new user
        app.MapPost("/api/register-user", async (User? newUser) =>
        {
            //This function will create a new record in the User table in the database using the details from newUser object that is passed as an arguement to this function.
            RegisterNewUser(User newUser);

            //check if they exist in the database and return User.username
            if (newUser == null)
            {
                return Results.BadRequest(new { Message = "Invalid user data. User cannot be null." });
            }
            else
            {
                //save to db
                bool isSaved = true;
                if (isSaved)
                {
                    return Results.Ok(new { Id = newUser.id, Username = newUser.username, Password = newUser.password });
                }
                else
                {
                    return Results.NotFound(new { Message = "User not found in the database" });
                }
            }        
        });
    }
}
