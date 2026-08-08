using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.Persistence
{
    public abstract class ApplicationDbContext : DbContext
    {
        protected ApplicationDbContext(
            DbContextOptions options)
            : base(options)
        {
        }
    }
}
