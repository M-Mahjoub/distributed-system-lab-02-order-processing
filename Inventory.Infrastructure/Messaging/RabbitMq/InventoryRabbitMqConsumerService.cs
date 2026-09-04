using System.Text;
using System.Text.Json;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Contracts.Inventory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Inventory.Infrastructure.Messaging.RabbitMq;

public sealed class InventoryRabbitMqConsumerService
    : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IServiceScopeFactory _scopeFactory;

    public InventoryRabbitMqConsumerService(
        IConnection connection,
        IServiceScopeFactory scopeFactory)
    {
        _connection = connection;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        await using var channel =
            await _connection.CreateChannelAsync(
                cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queue: "inventory.release",
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken);

        var consumer =
            new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, args) =>
        {
            await ProcessMessageAsync(
                args,
                stoppingToken);
        };

        await channel.BasicConsumeAsync(
            queue: "inventory.release",
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        await Task.Delay(
            Timeout.Infinite,
            stoppingToken);
    }

    private async Task ProcessMessageAsync(
        BasicDeliverEventArgs args,
        CancellationToken cancellationToken)
    {
        try
        {
            var message =
                JsonSerializer.Deserialize<
                    ReleaseInventoryIntegrationCommand>(
                    args.Body.Span);

            if (message is null)
                throw new InvalidOperationException(
                    "Invalid ReleaseInventory message.");

            using var scope =
                _scopeFactory.CreateScope();

            var handler =
                scope.ServiceProvider
                    .GetRequiredService<
                        ITransactionalMessageHandler<
                            ReleaseInventoryIntegrationCommand>>();

            await handler.HandleAsync(
                message,
                cancellationToken);

            await using var channel =
                await _connection.CreateChannelAsync(
                    cancellationToken: cancellationToken);

            await channel.BasicAckAsync(
                args.DeliveryTag,
                multiple: false,
                cancellationToken);
        }
        catch
        {
            // فعلاً NACK
            // سیاست Retry/DLQ را در مرحله بعد اضافه می‌کنیم.
            await using var channel =
                await _connection.CreateChannelAsync(
                    cancellationToken: cancellationToken);

            await channel.BasicNackAsync(
                args.DeliveryTag,
                multiple: false,
                requeue: true,
                cancellationToken);
        }
    }
}