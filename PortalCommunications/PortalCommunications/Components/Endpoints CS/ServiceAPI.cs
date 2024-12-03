using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.Threading.Tasks;
using PortalCommunications.Components.Pages;
using PortalCommunications.Models;


public class ServiceAPI
{
    private readonly HttpClient _httpClient;

    // Constructor to inject HttpClient
    public ServiceAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    //this function calls /api/user from login page
    //<!--Footnote: Change the way login task gets the data IF Home Team implements register user-->
    public async Task<User> LoginTask (int id, string username, string password)
    {
        //create payload - User object
        var loginPayload = new User {Id = id, FirstName = username, Password = password}; 

        var response = await _httpClient.PostAsJsonAsync("/api/user", loginPayload);

        if(response.IsSuccessStatusCode) //check for success response
        {
            var responseBody = await response.Content.ReadFromJsonAsync<User>();
            if(responseBody != null)
            {
                return loginPayload;
            } 
            else
            {
                throw new Exception("Login failed: No user data returned.");
            }
        }
        else
        {
            // Return an error if the response is not successful
            throw new Exception("Login failed: Invalid credentials.");
        }
    }
}
