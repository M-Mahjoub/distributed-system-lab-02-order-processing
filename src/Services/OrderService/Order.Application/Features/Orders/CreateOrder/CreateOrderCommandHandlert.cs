using BuildingBlocks.Application.Abstractions.Persistence;
using MediatR;
using Order.Application.Abstractions.Persistence;
using Order.Domain;
using Order.Domain.Abnstractions;
using Order.Domain.Orders;
using Order.Domain.Sagas;

namespace Order.Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderId>
    {
        public IOrderRepository _orderRepository { get; set; }
        public IOrderSagaRepository _orderSagaRepository { get; set; }

        public IUnitOfWork _unitOfWork { get; set; }

        public CreateOrderCommandHandler(
            IOrderSagaRepository orderSagaRepository,
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork)
        {
            _orderSagaRepository = orderSagaRepository;
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

            var saga = new OrderSaga(orderId.Value);

            await _orderRepository.AddAsync(order.Value, cancellationToken);

            await _orderSagaRepository.AddAsync(
                                            saga,
                                            cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return orderId;
        }
    }
}
