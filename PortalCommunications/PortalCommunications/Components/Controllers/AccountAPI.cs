using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
//using NuGet.Configuration;
//using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace BlazorApp1.Components.Controllers
{
    [Route("api/signup/[controller]")]
    [ApiController]
    
    public class RegisterController : ControllerBase { 
        //GET api/signup/<valuescontroller>
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
        
        // POST api/signup/<ValuesController>
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] JsonObject Request)
        {
            //Take Request object as read only.
            Request.AsReadOnly();
            IActionResult result = BadRequest();
            
            //***********************Commented out since missing Class Reference/Definition
            //Account credentials;
            ////Convert JSON to a Model.
            //if(Request.ContainsKey("Username") && Request.ContainsKey("Password"))
            //{
            //    credentials = JsonSerializer.Deserialize<Account>(Request);
            //    Console.WriteLine("Username: " + credentials.Username);
            //    Console.WriteLine("Password: " + credentials.Password);

            //    //Verify model information and validate login
            //    if (credentials.Validate())
            //    {
            //        result = Ok();
            //    }

            //}
            //else
            //{
            //    Console.WriteLine("error");
            //}

            return result;
        }
        [HttpPatch("{id}")]
        public IActionResult Update(int id)
        {
            return BadRequest();
        }

        // DELETE api/signup/<ValuesController>/
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Magic number
            if(id == 9)
            {
                System.IO.File.WriteAllText(@"devices.json", string.Empty);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
