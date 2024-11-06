using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalCommunicationsAPI.Data;
using PortalCommunicationsAPI.Models;

namespace PortalCommunicationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortalDevicesController : ControllerBase
    {
        private readonly PortalCommunicationsDbContext _context;

        public PortalDevicesController(PortalCommunicationsDbContext context)
        {
            _context = context;
        }

        [HttpGet("devices")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        [HttpGet("logs")]
        public async Task<ActionResult<IEnumerable<DeviceLog>>> GetDeviceLogs()
        {
            return await _context.DeviceLogs.ToListAsync();
        }

    }
}
