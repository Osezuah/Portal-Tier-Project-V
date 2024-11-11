using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace BlazorApp1.Components.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController: ControllerBase
    {
        [HttpGet]
        public JsonObject Index()
        {
                //Open file containing list of json formatted smart devices.
                string fileName = "devices.json";

                //Copy Text to a string
                string jsonString = System.IO.File.ReadAllText(fileName);

                //Turn them into a json object
                JsonObject list = JsonSerializer.Deserialize<JsonObject>(jsonString)!;


            Console.WriteLine("Device List Accesssed.");

            //Return JSON object
            return list;
           
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] JsonObject Request)
        {
            //****************Commented out since missing class reference
            ////Set Request object as readonly to protect integrity.
            //Request.AsReadOnly();

            ////Convert JSON into SmartDevice object
            //SmartDevice item = JsonSerializer.Deserialize<SmartDevice>(Request);

            ////Save to Json File.
            //item.registerDevice();

            Console.WriteLine("New Device Registered.");

            return Ok("Device Registered");
        }
    }
}
