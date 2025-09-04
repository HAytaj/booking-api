using Booking.Application.Abstract;
using Booking.Application.Concrete;
using Booking.Infrastructure.InMemory;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking API", Version = "v1" });
});

// DI
builder.Services.AddSingleton<IHomeRepository, InMemoryHomeRepository>();
builder.Services.AddSingleton<IAvailabilityService, AvailabilityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

// enable Swagger only in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API v1");
    });
}


app.MapControllers();

app.Run();

public partial class Program { }