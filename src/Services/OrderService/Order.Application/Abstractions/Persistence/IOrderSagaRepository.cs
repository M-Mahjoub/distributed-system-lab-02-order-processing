using Order.Domain.Sagas;

namespace Order.Application.Abstractions.Persistence
{
    public interface IOrderSagaRepository
    {
        Task<OrderSaga?> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            OrderSaga saga,
            CancellationToken cancellationToken = default);
    }
}
