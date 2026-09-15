using EasyNetQ;
using Messages;

namespace NotificationService.Infrastructure;

public class MessageHandler : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        Console.WriteLine("MessageHandler starting...");

        var bus = RabbitHutch.CreateBus("host=localhost");

        Console.WriteLine("Connected to RabbitMQ");

        await bus.PubSub.SubscribeAsync<PingMessage>(
            "ping-message",
            msg =>
            {
                Console.WriteLine(
                    $"Received message: {msg.Message}");
            },
            stoppingToken);

        Console.WriteLine("Subscribed to ping-message");

        try
        {
            await Task.Delay(
                Timeout.Infinite,
                stoppingToken);
        }
        catch (OperationCanceledException)
        {
            // Application is shutting down
        }

        Console.WriteLine("Message handler is stopping...");
    }
}