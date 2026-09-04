using BuildingBlocks.Application;
using BuildingBlocks.Application.Abstractions.Persistence;
using BuildingBlocks.Domain.Errors;
using Inventory.Application.Abstractions.Persistence;
using Inventory.Domain;
using Inventory.Domain.Abnstractions;
using MediatR;

namespace Inventory.Application.Features.Inventory.Reserve
{
    public sealed class ReserveInventoryCommandHandler
    : IRequestHandler<ReserveInventoryCommand, Result>
    {
        private readonly IProductInventoryRepository _repository;
        public IUnitOfWork _unitOfWork { get; set; }


        public ReserveInventoryCommandHandler(IUnitOfWork unitOfWork,
            IProductInventoryRepository repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            ReserveInventoryCommand command,
            CancellationToken cancellationToken)
        {
            var productIds = command.Items
                .Select(x => x.ProductId)
                .Distinct()
                .ToArray();

            var inventories =
                await _repository.GetByProductIdsAsync(
                    productIds,
                    cancellationToken);

            if (inventories.Count != productIds.Length)
            {
                return Result.Failure(
                    InventoryErrors.ProductNotFound(null));
            }

            foreach (var item in command.Items)
            {
                var inventory = inventories.First(
                    x => x.ProductId == item.ProductId);

                var result = inventory.Reserve(
                    command.OrderId,
                    item.Quantity);

                if (!result.IsSuccess)
                    return result;
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}
