using BuildingBlocks.Application;
using BuildingBlocks.Domain.Errors;
using Inventory.Domain;
using Inventory.Domain.Abnstractions;
using MediatR;

namespace Inventory.Application.Features.Inventory.Reserve
{
    public class ReserveInventoryCommandHandler : IRequestHandler<ReserveInventoryCommand, Result>
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IInventoryRepository _inventoryRepository { get; set; }

        public ReserveInventoryCommandHandler(IUnitOfWork unitOfWork, IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ReserveInventoryCommand command, CancellationToken cancellationToken)
        {
            var pIds = command.Items
                .Select(c => ProductId.From(c.ProductId).Value)
                .ToList();

            var inventories = await _inventoryRepository.GetByProductIdsAsync(pIds, cancellationToken);
            foreach (var item in command.Items)
            {
                var productId =
                 ProductId.From(item.ProductId).Value;

                if (!inventories.ContainsKey(productId))
                {
                    return Result.Failure(
                        InventoryErrors.ProductNotFound(productId));
                }

                var inventory =
                    inventories[productId];

                inventory.Reserve(item.Quantity);

            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}
