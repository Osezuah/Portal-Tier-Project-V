using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text.Json;
namespace APISeperateFiles;

public class APIEndpoints
{
    public static void Map(WebApplication app)
    {
        //route that accepts changes made to devices on Home
        app.MapPut("/api/device-status/", async (JsonElement JSobject) =>
        {

        });

        //This will be used to get a specific device
        app.MapGet("/api/{id}", async (JsonElement JSobject) =>
        {

        });

        //Create a route that will authenticate user login credentials - Go to line 33 in Login.razor to chaange the formaction link Thomas

        //Create a route that sends changes user makes to device in the ui to home application
        app.MapPut("/api/device-changes/", async (JsonElement JSobject) =>
        {

        });

        //Create a route that registers devices
        //Expecting a single device within the passed JSON Element.
        app.MapPost("/api/register-devices/", async (JsonDocument payload) =>
        {
            
        });


        //create a route that registers a new user
        app.MapPost("/api/register-user/", async (JsonElement JSobject) =>
        {

        });
    }
}
