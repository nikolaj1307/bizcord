using ChannelService.Data;
using ChannelService.Infrastructure;
using ChannelService.Service;
using MessageClient.Configuration;
using MessageClient.Factories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IChannelRepository, ChannelChannelRepository>();
builder.Services.AddScoped<IChannelService, ChannelService.Service.ChannelService>();
builder.Services.AddSingleton<IMessagePublisher>(new MessagePublisher(
    RabbitMQFactory.Create(
        new MessageClientOptions { ConnectionString = builder.Configuration["AppOptions:RABBITMQ_HOST"], MessagingProvider = MessagingProvider.RabbitMQ}
    )
));

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