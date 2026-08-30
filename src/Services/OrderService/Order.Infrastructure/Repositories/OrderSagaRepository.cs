using Microsoft.EntityFrameworkCore;
using Order.Application.Abstractions.Persistence;
using Order.Domain.Sagas;
using Order.Infrastructure.Persistence.DbContexts;

namespace Order.Infrastructure.Repositories
{
    public sealed class OrderSagaRepository
        : IOrderSagaRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderSagaRepository(
            OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderSagaState?> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.OrderSagaStates
                .FirstOrDefaultAsync(
                    x => x.OrderId == orderId,
                    cancellationToken);
        }

        public async Task AddAsync(
            OrderSagaState saga,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.OrderSagaStates.AddAsync(
                saga,
                cancellationToken);
        }
    }
}
