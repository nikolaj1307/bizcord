using MessageClient.Configuration;
using MessageClient.Extensions;
using MessageClient.Interfaces;
using Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessageClient(options =>
{
    options.MessagingProvider = MessagingProvider.RabbitMQ;
    options.ConnectionString =
        builder.Configuration["RabbitMQ:ConnectionString"] ?? "host=localhost";
});

var app = builder.Build();

var messageClient = app.Services.GetRequiredService<IMessageClient>();

await messageClient.SubscribeAsync<PingMessage>(
    "ping",
    message => Console.WriteLine($"Received PingMessage: {message.Message}"));

await messageClient.SubscribeAsync<PongMessage>(
    "pong",
    message => Console.WriteLine($"Received PongMessage: {message.Message}"));

// Calling /ping sends a PongMessage; calling /pong sends a PingMessage.
app.MapPost("/PingPong/ping", async (
    IMessageClient client, CancellationToken cancellationToken) =>
{
    await client.PublishAsync(
        "pong", new PongMessage { Message = "ping" }, cancellationToken);
    return Results.Ok("pong");
});

app.MapPost("/PingPong/pong", async (
    IMessageClient client, CancellationToken cancellationToken) =>
{
    await client.PublishAsync(
        "ping", new PingMessage { Message = "pong" }, cancellationToken);
    return Results.Ok("ping");
});

try
{
    await app.RunAsync();
}
finally
{
    await messageClient.UnsubscribeAsync("ping");
    await messageClient.UnsubscribeAsync("pong");
}