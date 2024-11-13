using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text.Json;
namespace APISeperateFiles;

public class APIEndpoints
{
    public static void Map(WebApplication app)
    {
        ////route that accepts changes made to devices on Home
        //app.MapPut("/api/device-status/", async (JsonElement JSobject) =>
        //{

        //});

        //This will be used to get a specific device
        app.MapGet("/api/device/{int Idnumber}", async (Idnumber) =>
        {

        });

        ////Create a route that will authenticate user login credentials - Go to line 33 in Login.razor to chaange the formaction link Thomas
        ///
        app.MapGet("/api/user", async (JsonElement JSobject) =>
        {
        });

        ////Create a route that sends changes user makes to device in the ui to home application
        //app.MapPut("/api/device-changes/", async (JsonElement JSobject) =>
        //{

        //});

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
        //app.MapPost("/api/register-user/", async (JsonElement JSobject) =>
        //{

        //});
    }
}
