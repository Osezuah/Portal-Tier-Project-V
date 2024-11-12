using System.Runtime.InteropServices;
using System.Text.Json;
namespace APISeperateFiles;

public class APIEndpoints
{
    public static void Map(WebApplication app) 
    {
        app.MapPut("/api/device-status/", async(JsonElement JSobject) =>
        {
            JSobject.TryGetProperty("device-type", out JsonElement DT);
            string Type = DT.GetString();

            return Results.Ok(new {message = "Device Type is:", Type});

            //BUsiness layer's commands - Give them Objec
           // return Results.Ok(JSobject);
        })
        .WithName("ReceiveDeviceStatus")
        .WithTags("Device");
       
        //app.MapGet("/", async context =>
        //{
        //    //A get Action
        //    await context.Response.WriteAsJsonAsync(new { Message = "All todo items." });
        //});

        //This will be used to get a specific device
        app.MapGet("/api/{id}", async context =>
        {
            // Get one todo item

            await context.Response.WriteAsJsonAsync(new { Message = "One todo item" });
        });
        //Create a route that will authenticate user login credentials.
        //Go to line 33 in Login.razor to chaange the formaction link


    }
}

public record DeviceStatus(String device_name, String location, String status, double? temperature);