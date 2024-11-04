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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }
    }
}
