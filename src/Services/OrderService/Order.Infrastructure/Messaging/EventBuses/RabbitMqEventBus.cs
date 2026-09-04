using BuildingBlocks.Application.Messaging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

namespace Order.Infrastructure.Messaging.EventBuses
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly RabbitMqOptions _mqOptions;
        private readonly IConnection _connection;
        public RabbitMqEventBus(IOptions<RabbitMqOptions> options)
        {
            _mqOptions = options.Value;
        }
        public async Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _mqOptions.HostAddress,
                Port = 5672,
                UserName = _mqOptions.Usename,
                Password = _mqOptions.Password

            };
            try
            {

                using var _connection = await factory.CreateConnectionAsync(cancellationToken: cancellationToken);
                using var channel = await _connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
               queue: eventType,
               durable: true,
               exclusive: false,
                autoDelete: false,
               cancellationToken: cancellationToken);


                var body =
               Encoding.UTF8.GetBytes(payload);

                var properties =
                    new BasicProperties
                    {
                        Persistent = true,
                        Type = eventType
                    };

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: eventType,
                    mandatory: true,
                    basicProperties: properties,
                    body: body,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
