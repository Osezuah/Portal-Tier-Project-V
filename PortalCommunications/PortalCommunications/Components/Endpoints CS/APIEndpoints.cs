using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text.Json;
using PortalCommunications.Components.Device_Class;
using PortalCommunications.Components.Pages;
using Microsoft.AspNetCore.Mvc;
using PortalCommunications;
using PortalCommunications.Models;

namespace APISeperateFiles;

public class APIEndpoints
{
    //private readonly PortalDeviceContext portalDeviceContext;
    PortalCommunications.PortalCADInterface interface_object;



        public APIEndpoints(PortalCADInterface interfaceObject)
        {
            interface_object = interfaceObject;
        }


    public static void Map(WebApplication app)
    {
        ////route that accepts changes made to devices on Home
               app.MapPut("/api/device-status/", async (PortalCommunications.Device newDevice, PortalCADInterface interface_object) =>
                {
                    
                    if (newDevice == null)
                    {
                        return Results.BadRequest(new { message = "Invalid data!"});
                    }
                    string device_name = newDevice.Name;

                    /*                    var options = new JsonSerializerOptions
                                        {
                                            PropertyNameCaseInsensitive = true // This ensures case-insensitive matching
                                        };*/
                    PortalCommunications.DeviceLog deviceLog = newDevice.Logs.ElementAtOrDefault(0);

                    if (device_name.Contains("Lock"))
                    {
                        //Hardcoding some values becuase of incompatibility of both device classes
                        Lock lock_ = new Lock(newDevice.Id, newDevice.Name, true, newDevice.LastUpdated, 1, deviceLog.LoggedState);
                        if (lock_.Validate())
                        {
                            if (interface_object.UpdateChangesInDatabase(newDevice)){
                                return Results.BadRequest(new { message = "Device Information Updated Successfully!" });
                            }
                            else
                            {
                                return Results.BadRequest(new { message = "Device Information could not be Updated!" });
                            }
                        }
                        else
                        {
                            return Results.BadRequest(new { message = "Invalid data!" });
                        }

                    }
                    else if(device_name.Contains("Sensor"))
                    {
                        Sensors sensor = new Sensors(newDevice.Id, newDevice.Name, true, newDevice.LastUpdated, 1, deviceLog.LoggedState);
                        if (sensor.Validate())
                        {
                            if (interface_object.UpdateChangesInDatabase(newDevice))
                            {
                                return Results.BadRequest(new { message = "Device Information Updated Successfully!" });
                            }
                            else
                            {
                                return Results.BadRequest(new { message = "Device Information could not be Updated!" });
                            }
                        }
                        else
                        {
                            return Results.BadRequest(new { message = "Invalid data!" });
                        }
                    }
                    else if (device_name.Contains("Camera"))
                    {
                        //didn't have validation for this class, therefore directly passing this to database for update
                        if (interface_object.UpdateChangesInDatabase(newDevice))
                        {
                            return Results.BadRequest(new { message = "Device Information Updated Successfully!" });
                        }
                        else
                        {
                            return Results.BadRequest(new { message = "Device Information could not be Updated!" });
                        }
                        
                    }
                    else if (device_name.Contains("Alarm"))
                    {
                        Alarm alarm = new Alarm(newDevice.Id, newDevice.Name, true, newDevice.LastUpdated, 1, deviceLog.LoggedState);
                        if (alarm.Validate())
                        {
                            if (interface_object.UpdateChangesInDatabase(newDevice))
                            {
                                return Results.BadRequest(new { message = "Device Information Updated Successfully!" });
                            }
                            else
                            {
                                return Results.BadRequest(new { message = "Device Information could not be Updated!" });
                            }
                        }
                        else
                        {
                            return Results.BadRequest(new { message = "Invalid data!" });
                        }
                    }
                    else if (device_name.Contains("Tracker"))
                    {
                        Tracker tracker = new Tracker(newDevice.Id, newDevice.Name, true, newDevice.LastUpdated, 1, deviceLog.LoggedState);
                        if (tracker.Validate())
                        {
                            if (interface_object.UpdateChangesInDatabase(newDevice))
                            {
                                return Results.BadRequest(new { message = "Device Information Updated Successfully!" });
                            }
                            else
                            {
                                return Results.BadRequest(new { message = "Device Information could not be Updated!" });
                            }
                        }
                        else
                        {
                            return Results.BadRequest(new { message = "Invalid data!" });
                        }
                    }
                    else if (device_name.Contains("Fridge"))
                    {
                        SmartFridge smartFridge = new SmartFridge(newDevice.Id, newDevice.Name, true, newDevice.LastUpdated, 1, deviceLog.LoggedState);
                        if (smartFridge.Validate())
                        {
                            if (interface_object.UpdateChangesInDatabase(newDevice))
                            {
                                return Results.BadRequest(new { message = "Device Information Updated Successfully!" });
                            }
                            else
                            {
                                return Results.BadRequest(new { message = "Device Information could not be Updated!" });
                            }
                        }
                        else
                        {
                            return Results.BadRequest(new { message = "Invalid data!" });
                        }
                    }
                    else if (device_name.Contains("Dehumidifier"))
                    {
                        //didn't have validation for this class, therefore directly passing this to database for update
                        if (interface_object.UpdateChangesInDatabase(newDevice))
                        {
                            return Results.BadRequest(new { message = "Device Information Updated Successfully!" });
                        }
                        else
                        {
                            return Results.BadRequest(new { message = "Device Information could not be Updated!" });
                        }
                    }
                    else if (device_name.Contains("Thermostat"))
                    {
                        //didn't have validation for this class, therefore directly passing this to database for update
                        if (interface_object.UpdateChangesInDatabase(newDevice))
                        {
                            return Results.BadRequest(new { message = "Device Information Updated Successfully!" });
                        }
                        else
                        {
                            return Results.BadRequest(new { message = "Device Information could not be Updated!" });
                        }
                    }


                    // This functions will make a new record in "Device Activity" table with new updated details. This "newDeviceObjectWithUpdatedDetails" object contains the update/new details. This function is called whenever a change is made by Home or Portal UI to any of the devices, and this would return a "true" if the changes are made successfully.
                    // UpdateChangesInDatabase(Device newDeviceObjectWithUpdatedDetails);

                    return Results.BadRequest(new { message = "Device Information Updated Successfully!" });
                });

            //This will be used to get a specific device
            app.MapGet("/api/device/{Idnumber}", async (int Idnumber, PortalCADInterface interface_object) =>
            {
                    
                PortalCommunications.Device device = interface_object.GetDeviceById(Idnumber);

                if (device == null)
                {
                    return Results.NotFound(new { Message = "Device with this Id not found in database!" });
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var jsonResponse = JsonSerializer.Serialize(device, options);
                return Results.Json(device);
            });
        

        ////Create a route that will authenticate user login credentials - Go to line 33 in Login.razor to chaange the formaction link Thomas
        //app.MapPost("/api/user", async ([FromBody] JsonDocument JSobject) =>
        app.MapPost("/api/user", async (PortalCommunications.Models.User newUser, [FromServices] PortalCADInterface interface_object) =>
        {

                    //PortalCommunications.Components.Device_Class.User user = JsonSerializer.Deserialize<PortalCommunications.Components.Device_Class.User>(JSobject.RootElement.GetRawText());

                    //check if they exist in the database and return User.username
                    if (newUser == null) {
                        return Results.BadRequest(new { Message = "Invalid user data." });
                    }

                    if (interface_object.RegisterNewUser(newUser)){ //just changed it to register the user to database
                        //return Results.Ok(new { Id = user.id, Username = user.username, Password = user.password });
                        return Results.Ok(new { Message = "User Authenticated!" });
                    }
                    else{
                        return Results.NotFound(new { Message = "User not found in the database" });
                    }

                });

        ////Create a route that sends changes user makes to device in the ui to home application
        app.MapPut("/api/device-changes/", async ([FromBody] JsonElement JSobject, PortalCADInterface interface_object) =>
        {
            try
            {
                // Parse the input JSON
                var deviceId = JSobject.GetProperty("id").GetInt32();
                var newState = JSobject.GetProperty("newState").GetString();

                // Use the interface to get the device by ID
                var device = interface_object.GetDeviceById(deviceId);
                if (device == null)
                {
                    return Results.NotFound(new { Message = $"Device with ID {deviceId} not found in the database!" });
                }

                // Update the device state and last updated timestamp
                device.State = newState;
                device.LastUpdated = DateTime.UtcNow;
                   
                // Save changes back to the database
                bool updateSuccess = interface_object.UpdateChangesInDatabase(device);
                if (!updateSuccess)
                {
                    return Results.Json(new { Message = "Failed to update the device in the database." }, statusCode: 500);
                }

                // Return a success message
                return Results.Ok(new { Message = $"Device {deviceId} state updated to {newState} in the database." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating device: {ex.Message}");
                return Results.Json(new { Message = "Failed to update the device in the database." }, statusCode: 500);
            }
        });




        //Create a route that registers devices
        //Expecting a single device within the passed JSON Element.
        //app.MapPost("/api/register-device/", async (JsonDocument payload, PortalCADInterface interface_object) =>
        app.MapPost("/api/register-device/", async (PortalCommunications.Device newDevice, PortalCADInterface interface_object) =>
                {


                    if (interface_object.RegisterNewDevice(newDevice))
                    {
                        //Return message
                        return Results.Ok(new { message = "Device Successfully Registered!" });
                    }
                    else
                    {
                        return Results.Ok(new { message = "Device Registration not successful!" });
                    }
                });

        ////create a route that registers a new user
        // app.MapPost("/api/register-user", async (PortalCommunications.Components.Device_Class.User newUser) =>
        app.MapPost("/api/register-user", async (PortalCommunications.Models.User newUser, PortalCADInterface interface_object) =>
                {

                    //check if they exist in the database and return User.username
                    if (newUser == null)
                    {
                        return Results.BadRequest(new { Message = "Invalid user data. User cannot be null." });
                    }
                    
                    //This function will create a new record in the User table in the database using the details from newUser object that is passed as an arguement to this function.
                    bool isSaved = interface_object.RegisterNewUser(newUser);
                    //bool isSaved = true;
                    if (isSaved)
                    {
                        //return Results.Ok(new { id = newUser.Id, firstname = newUser.FirstName, lastname = newUser.LastName, email = newUser.Email, password = newUser.Password });
                        return Results.Ok(new { message = "User Successfully Registered!" });
                    }
                    else
                    {
                        return Results.NotFound(new { Message = "User Already Exists!" });
                    }

                });


        //ENDPOINT MADE FOR TESTING
    app.MapGet("/api/devices", async (PortalDeviceContext db) =>
        {
            var devices = db.devices.ToList();
            return Results.Ok(devices);
        });


    }
}
