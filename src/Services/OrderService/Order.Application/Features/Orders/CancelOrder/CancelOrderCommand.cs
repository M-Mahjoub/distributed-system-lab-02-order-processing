using BuildingBlocks.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.CancelOrder
{
    public sealed record CancelOrderCommand(
     Guid OrderId)
     : IRequest<Result>;
}
