using Microsoft.EntityFrameworkCore;
using Order.Domain.Abnstractions;
using Order.Domain.Orders;
using Order.Infrastructure.Persistence.DbContexts;

namespace Order.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private OrderDbContext _context { get; set; }

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Domain.Orders.Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
        }

        public async Task<Domain.Orders.Order> GetByIdAsync(OrderId id, CancellationToken cancellationToken)
        {
            return await _context.Orders.FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
        }
    }
}
