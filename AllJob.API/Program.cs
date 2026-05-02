using AllJob.API.Extensions;
using AllJob.API.Hubs;
using AllJob.Application.Extensions;
using AllJob.Persistence.Extensions;
using AllJob.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddSerilogLogging();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApiService(builder.Configuration);
builder.Services.AddHangfireServices(builder.Configuration);
builder.Services.AddSignalR();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<AllJob.Persistence.Context.AppDbContext>();
    await db.Database.MigrateAsync();
}


var retryCount = 0;
while (retryCount < 5)
{
    try
    {
        await DataSeeder.SeedAsync(app.Services);
        break;
    }
    catch (Exception)
    {
        retryCount++;
        Console.WriteLine($"DB not ready, retry {retryCount}/5...");
        await Task.Delay(TimeSpan.FromSeconds(15));
        if (retryCount == 5) throw;
    }
}

app.UseExceptionMiddleware();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireServices();
app.UseRateLimiter();

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");
app.Run();