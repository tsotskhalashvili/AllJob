using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Persistence.Context;
using AllJob.Persistence.Interceptors;
using AllJob.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AllJob.Persistence.Extensions;

public static class PersistenceServiceRegistration
{

    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<SoftDeleteInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
     options.UseSqlServer(
         configuration.GetConnectionString("DefaultConnection"))
         .AddInterceptors(
             sp.GetRequiredService<AuditableEntityInterceptor>(),
             sp.GetRequiredService<SoftDeleteInterceptor>()));

        services.AddScoped(typeof(IGenericRepository<>),
            typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        return services;
            

    }
}
