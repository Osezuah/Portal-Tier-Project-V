using Microsoft.EntityFrameworkCore;
using PortalCommunicationsAPI.Models;

namespace PortalCommunicationsAPI.Data
{
    public class PortalCommunicationsDbContext : DbContext
    {
        public PortalCommunicationsDbContext(DbContextOptions<PortalCommunicationsDbContext> options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }

        // Define DbSets for each entity/table, e.g., Users
        public DbSet<User> Users { get; set; }
    }

}
