using Microsoft.EntityFrameworkCore;

namespace ransomware_webapi.Models
{
    public class RansomwareDBContext : DbContext
    {
        public DbSet<RansomwareDBContext> Ransomwares { get; set; }

        public RansomwareDBContext(DbContextOptions<RansomwareDBContext> options)
            : base(options)
        {
        }

    }
}
