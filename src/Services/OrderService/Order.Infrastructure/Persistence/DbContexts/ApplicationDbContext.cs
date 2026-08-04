using Microsoft.EntityFrameworkCore;

namespace Order.Infrastructure.Persistence.DbContexts
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
