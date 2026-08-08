using Order.Domain.Orders;

namespace Order.Domain.Abnstractions
{
    public interface IOederRepository : IRepository<Order.Domain.Orders.Order, OrderId>
    {
        Task<Order.Domain.Orders.Order> GetByIdAsync(OrderId id, CancellationToken cancellationToken);

        //Task<bool> ExistsByOrderNumberAsync(OrderNumber orderNumber, CancellationToken cancellationToken);
    }
}
