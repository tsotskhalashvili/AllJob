using AllJob.Application.Settings;
using AllJob.Domain.Entities.Auth;
using AllJob.Domain.Enums;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AllJob.Persistence.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();
        var adminSettings = scope.ServiceProvider
            .GetRequiredService<IOptions<AdminSettings>>().Value;

        await SeedRolesAsync(context);
        await context.SaveChangesAsync();

        await SeedSuperAdminAsync(context, adminSettings);
        await context.SaveChangesAsync();
    }

    private static async Task SeedRolesAsync(AppDbContext context)
    {
        if (await context.Roles.AnyAsync()) return;

        var roles = Enum.GetValues<RoleType>()
            .Select(r => new Role
            {
                Id = Guid.NewGuid(),
                Name = r.ToString()
            });

        await context.Roles.AddRangeAsync(roles);
    }

    private static async Task SeedSuperAdminAsync(
        AppDbContext context, AdminSettings settings)
    {
        if (await context.Users.AnyAsync(u => u.Email == settings.Email))
            return;

        var superAdminRole = await context.Roles
            .FirstOrDefaultAsync(r => r.Name == RoleType.SuperAdmin.ToString());

        if (superAdminRole is null) return;

        var superAdmin = new User
        {
            Id = Guid.NewGuid(),
            Email = settings.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(settings.Password),
            IsActive = true,
            IsPasswordChangeRequired = false
        };

        await context.Users.AddAsync(superAdmin);
        await context.SaveChangesAsync();

        await context.UserRoles.AddAsync(new UserRole
        {
            UserId = superAdmin.Id,
            RoleId = superAdminRole.Id
        });
    }
}
