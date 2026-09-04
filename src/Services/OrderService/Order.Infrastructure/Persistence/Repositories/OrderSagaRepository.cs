using Microsoft.EntityFrameworkCore;
using Order.Application.Abstractions.Persistence;
using Order.Domain.Sagas;
using Order.Infrastructure.Persistence.DbContexts;

namespace Order.Infrastructure.Persistence.Repositories;

public sealed class OrderSagaRepository : IOrderSagaRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderSagaRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<OrderSaga?> GetByOrderIdAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.OrderSagas
            .SingleOrDefaultAsync(
                x => x.OrderId == orderId,
                cancellationToken);
    }

    public async Task AddAsync(
        OrderSaga saga,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.OrderSagas.AddAsync(
            saga,
            cancellationToken);
    }
}