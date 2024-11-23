using System.Net.Http.Json;
using PortalCommunications.Components.Device_Class;

public class ServiceAPI
{
    private readonly HttpClient _httpClient;

    // Constructor to inject HttpClient
    public ServiceAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    //this function calls /api/user from login page
    public async Task<User> LoginTask (int id, string username, string password)
    {
        //create payload - User object
        var loginPayload = new User {id = id, username = username, password = password}; 

        var response = await _httpClient.PostAsJsonAsync("/api/user", loginPayload);

        if(response.IsSuccessStatusCode) //check for success response
        {
            var responseBody = await response.Content.ReadFromJsonAsync<User>();
            if(responseBody != null)
            {
                //initialise payload with json data parsed
                loginPayload.id = responseBody.id;
                loginPayload.username = responseBody.username;
                loginPayload.password = responseBody.password;

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
