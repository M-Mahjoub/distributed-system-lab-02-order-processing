using BuildingBlocks.Domain.Errors;
using Order.Domain.Abnstractions;
using Order.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Domain.Orders;
using BuildingBlocks.Application.Abstractions.Persistence;

namespace Order.Application.Features.Orders.ConfirmOrder
{
    public sealed class ConfirmOrderCommandHandler
    : IRequestHandler<ConfirmOrderCommand, Result>
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmOrderCommandHandler(
            IOrderRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            ConfirmOrderCommand command,
            CancellationToken cancellationToken)
        {
            var orderId = OrderId.From(command.OrderId);
            if (!orderId.IsSuccess)
                return Result.Failure(OrderErrors.InvalidId);

            var order =
                await _repository.GetByIdAsync(
                    orderId.Value,
                    cancellationToken);

            if (order is null)
            {
                //return Result.Failure(
                //    OrderErrors.NotFound);
            }

            var result = order.Confirm();

            if (!result.IsSuccess)
                return result;

            await _unitOfWork.CommitAsync(
                cancellationToken);

            return Result.Success();
        }
    }
}
