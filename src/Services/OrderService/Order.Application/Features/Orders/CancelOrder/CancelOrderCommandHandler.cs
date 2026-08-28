using BuildingBlocks.Application;
using BuildingBlocks.Domain.Errors;
using Order.Domain.Abnstractions;
using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Orders;
using MediatR;

namespace Order.Application.Features.Orders.CancelOrder
{
    public sealed class CancelOrderCommandHandler
      : IRequestHandler<CancelOrderCommand, Result>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(
            IOrderRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            CancelOrderCommand command,
            CancellationToken cancellationToken)
        {

            var orderId = OrderId.From(command.OrderId);
            if (!orderId.IsSuccess)
                return Result.Failure(OrderErrors.InvalidId);

            var order =
                await _repository.GetByIdAsync(
                    orderId.Value,
                    cancellationToken);

            //if (order is null)
            //    return Result.Failure(OrderErrors.NotFound);

            var result = order.Cancel();

            if (!result.IsSuccess)
                return result;

            await _unitOfWork.CommitAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
