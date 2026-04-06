using AllJob.Application.Settings;
using AllJob.Domain.Entities.Auth;
using AllJob.Domain.Entities.Jobs;
using AllJob.Domain.Entities.Shared;
using AllJob.Domain.Enums.Auth;
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

        await SeedAddressesAsync(context);
        await context.SaveChangesAsync();

        await SeedSkillsAsync(context);
        await context.SaveChangesAsync();

        await SeedJobCategoriesAsync(context);
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

    private static async Task SeedAddressesAsync(AppDbContext context)
    {
        if (await context.Addresses.AnyAsync()) return;

        var addresses = new[]
        {
            ("Georgia", "Tbilisi"),
            ("Georgia", "Batumi"),
            ("Georgia", "Kutaisi"),
            ("Georgia", "Rustavi"),
            ("Georgia", "Gori")
        }.Select(a => new Address
        {
            Id = Guid.NewGuid(),
            Country = a.Item1,
            City = a.Item2
        });

        await context.Addresses.AddRangeAsync(addresses);
    }

    private static async Task SeedSkillsAsync(AppDbContext context)
    {
        if (await context.Skills.AnyAsync()) return;

        var skills = new[]
        {
            "C#", "ASP.NET Core", "SQL",
            "JavaScript", "React", "Python",
            "Docker", "Azure", "Git", "Java"
        }.Select(s => new Skill
        {
            Id = Guid.NewGuid(),
            Name = s
        });

        await context.Skills.AddRangeAsync(skills);
    }

    private static async Task SeedJobCategoriesAsync(AppDbContext context)
    {
        if (await context.JobCategories.AnyAsync()) return;

        var categories = new[]
        {
            ("Information Technology", "information-technology"),
            ("Finance", "finance"),
            ("Marketing", "marketing"),
            ("Design", "design"),
            ("Services", "services")
        }.Select(c => new JobCategory
        {
            Id = Guid.NewGuid(),
            Name = c.Item1,
            Slug = c.Item2,
            IconUrl = string.Empty
        });

        await context.JobCategories.AddRangeAsync(categories);
    }
}