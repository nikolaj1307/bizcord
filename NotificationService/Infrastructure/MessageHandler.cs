using EasyNetQ;
using Messages;
using SharedModels;

namespace NotificationService.Infrastructure;

public class MessageHandler(IBus bus) : BackgroundService
{
    
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        Console.WriteLine("MessageHandler starting...");

        Console.WriteLine("Connected to RabbitMQ");

        await bus.PubSub.SubscribeAsync<ChannelUpdatedMessage>(
            "channel-updated",
            msg =>
            {
                Console.WriteLine(
                    $"Received message: {msg.ChannelId} - {msg.ChannelName}");
            },
            stoppingToken);

        Console.WriteLine("Subscribed to channel-updated-message");

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