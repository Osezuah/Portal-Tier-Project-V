using Microsoft.AspNetCore.Mvc;
using PortalCommunicationsAPI.Data;
using PortalCommunicationsAPI.Models;
using System.Threading.Tasks;

namespace PortalCommunicationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PortalCommunicationsDbContext _context;

        public UsersController(PortalCommunicationsDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }


        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
