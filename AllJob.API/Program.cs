using AllJob.API.Extensions;
using AllJob.Application.Extensions;
using AllJob.Persistence.Extensions;
using AllJob.Persistence.Seed;
using Serilog;



var builder = WebApplication.CreateBuilder(args);
builder.Host.AddSerilogLogging();

// Add services to the container.
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApiService(builder.Configuration);
builder.Services.AddHangfireServices(builder.Configuration);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


await DataSeeder.SeedAsync(app.Services);

app.UseExceptionMiddleware();
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();


app.UseAuthorization();
app.UseHangfireServices();
app.UseRateLimiter();

app.MapControllers();

app.Run();
