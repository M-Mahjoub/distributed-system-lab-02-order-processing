using BuildingBlocks.Contracts.IntegrationEvents.Orders;
using BuildingBlocks.Domain.Errors;
using MediatR;

namespace Inventory.Application.Features.Inventory.Reserve
{
    public sealed record ReserveInventoryCommand(
        Guid OrderId,
        IReadOnlyCollection<ReserveInventoryItem> Items) : IRequest<Result>
    {
    }
}
