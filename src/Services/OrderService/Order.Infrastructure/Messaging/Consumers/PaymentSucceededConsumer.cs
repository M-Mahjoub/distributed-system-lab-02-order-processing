using BuildingBlocks.Contracts.IntegrationEvents.Payments;
using MediatR;
using Order.Application.Features.Orders.ConfirmOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Messaging.Consumers
{
    public sealed class PaymentSucceededConsumer
    {
        private readonly ISender _sender;

        public PaymentSucceededConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task ConsumeAsync(
            PaymentSucceededIntegrationEvent message,
            CancellationToken cancellationToken)
        {
            var command = new ConfirmOrderCommand(
                message.OrderId);

            await _sender.Send(
                command,
                cancellationToken);
        }
    }
}
