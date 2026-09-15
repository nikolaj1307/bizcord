using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Infrastructure;
using NotificationService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NotificationContext>(options =>
    options.UseNpgsql(
        builder.Configuration["AppOptions:DbConnectionString"]
    ));

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService.Service.NotificationService>();

builder.Services.AddSingleton(
    RabbitHutch.CreateBus("host=localhost"));
builder.Services.AddHostedService<MessageHandler>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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