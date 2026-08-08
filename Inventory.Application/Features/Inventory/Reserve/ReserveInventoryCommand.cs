using BuildingBlocks.Domain.Errors;
using MediatR;
using Order.Contracts.dtos;

namespace Inventory.Application.Features.Inventory.Reserve
{
    public sealed record ReserveInventoryCommand(
        Guid EventId,
        Guid OrderId,
        IReadOnlyCollection<OrderItemDto> Items) : IRequest<Result>
    {
    }
}
