using Application.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Serilog;
using API.Middlewares;
using Domain.Entities;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentationLayer();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RestaurantsDBContext>();
    dbContext.Database.Migrate();
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
    await seeder.Seed();
}

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();
app.UseHttpsRedirection();

app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    // This class is used to allow the use of the `Program` class in the test project.
    // It is necessary for the test project to access the `Program` class without
    // having to reference the entire API project.
}