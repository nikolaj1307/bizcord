using ChannelService.Data;
using ChannelService.Service;
using MessageClient.Configuration;
using MessageClient.Factories;
using MessageClient.Interfaces;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using RabbitMQ.Client.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IChannelRepository, ChannelChannelRepository>();
builder.Services.AddScoped<IChannelService, ChannelService.Service.ChannelService>();
builder.Services.AddSingleton<IMessageClient>(
    RabbitMQFactory.Create(
        new MessageClientOptions
        {
            ConnectionString = builder.Configuration["AppOptions:RABBITMQ_HOST"],
            MessagingProvider = MessagingProvider.RabbitMQ
        }
    )
);

builder.Services.AddSingleton<ResiliencePipeline>(_=> new ResiliencePipelineBuilder()
    .AddRetry(new RetryStrategyOptions
    {
        ShouldHandle = new PredicateBuilder().Handle<Exception>(),
        MaxRetryAttempts = 5,
        Delay = TimeSpan.FromSeconds(5), //TODO: Make this higher for each retry attempt.
        OnRetry = static args =>
        {
            Console.WriteLine($"Retrying due to: {args.Outcome.Exception.GetType()}. Attempt {args.AttemptNumber}");
            return default;
        }
    })
    .Build()
);

builder.Services.AddDbContext<ChannelContext>(options =>
    options.UseNpgsql(
        builder.Configuration["AppOptions:DbConnectionString"]
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();