using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Messaging.Consumers
{
    public class OrderCreatedConsumer
    {
        //private readonly IMediator _mediator;

        //public OrderCreatedConsumer(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        //public async Task Consume(
        //    OrderCreatedIntegrationEvent message,
        //    CancellationToken cancellationToken)
        //{
        //    var command =
        //        new ReserveInventoryCommand(
        //            message.EventId,
        //            message.OrderId,
        //            message.Items);

        //    await _mediator.Send(command, cancellationToken);
        //}
    }
}
