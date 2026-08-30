using Order.Domain.Sagas;

namespace Order.Application.Abstractions.Persistence
{
    public interface IOrderSagaRepository
    {
        Task<OrderSagaState?> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            OrderSagaState saga,
            CancellationToken cancellationToken = default);
    }
}
