using PortalCommunicationsAPI.Models;

namespace PortalCommunications.Services
{
    public class DeviceService
    {

        private readonly HttpClient _httpClient;

        public DeviceService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("WebAPI");
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Device>>("api/devices");
        }

        public async Task AddDeviceAsync(Device device)
        {
            await _httpClient.PostAsJsonAsync("api/devices", device);
        }
    }
}
