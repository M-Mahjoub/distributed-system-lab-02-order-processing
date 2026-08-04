using MediatR;
using Order.Domain.Orders;

namespace Order.Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<OrderId>
    {

    }
}
