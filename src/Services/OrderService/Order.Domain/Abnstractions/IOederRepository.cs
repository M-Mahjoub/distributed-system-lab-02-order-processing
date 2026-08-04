using Order.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Abnstractions
{
    public interface IOederRepository : IRepository<Order.Domain.Orders.Order, OrderId>
    {
        Task<Order.Domain.Orders.Order> GetByIdAsync(OrderId id, CancellationToken cancellationToken);

        //Task<bool> ExistsByOrderNumberAsync(OrderNumber orderNumber, CancellationToken cancellationToken);
    }
}
