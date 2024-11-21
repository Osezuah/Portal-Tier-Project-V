using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace PortalCommunications.Components.Services
{
    public class AccountService
    {
        private readonly HttpClient _http;

        public AccountService(HttpClient http)
        {
            _http = http;
        }

        // Method to delete a user account by ID
        public async Task<bool> DeleteAccountAsync(int userId)
        {
            var response = await _http.DeleteAsync($"api/users/{userId}");
            return response.IsSuccessStatusCode;
        }

    }
}
