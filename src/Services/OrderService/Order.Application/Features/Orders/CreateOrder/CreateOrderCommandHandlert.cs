using BuildingBlocks.Application;
using MediatR;
using Order.Domain;
using Order.Domain.Abnstractions;
using Order.Domain.Orders;

namespace Order.Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderId>
    {
        public IOrderRepository _orderRepository { get; set; }

        public IUnitOfWork _unitOfWork { get; set; }

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        async Task<OrderId> IRequestHandler<CreateOrderCommand, OrderId>.Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var orderId = OrderId.New();
            var customerId = CustomerId.New();
            var order = Order.Domain.Orders.Order.Create(orderId, customerId);

            await _orderRepository.AddAsync(order.Value, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return orderId;
        }
    }
}
