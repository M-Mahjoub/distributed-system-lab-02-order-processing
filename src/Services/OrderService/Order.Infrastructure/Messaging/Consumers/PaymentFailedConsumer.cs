using BuildingBlocks.Contracts.IntegrationEvents.Payments;
using MediatR;
using Order.Application.Features.Orders.CancelOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Messaging.Consumers
{
    public sealed class PaymentFailedConsumer
    {
        private readonly ISender _sender;

        public PaymentFailedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task ConsumeAsync(
            PaymentFailedIntegrationEvent message,
            CancellationToken cancellationToken)
        {
            var command = new CancelOrderCommand(
                message.OrderId);

            await _sender.Send(
                command,
                cancellationToken);
        }
    }
}
