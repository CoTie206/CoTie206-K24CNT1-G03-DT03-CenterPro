using CenterPro.Data.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CenterPro.Web.Infrastructure;

public static class DatabaseBootstrapper
{
    private static readonly string[] Roles = ["Lecturer", "TrainingStaff", "Administrator"];

    public static async Task InitializeAsync(IServiceProvider services, IConfiguration configuration)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<CenterProDbContext>();
        await db.Database.MigrateAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<NguoiDungUngDung>>();
        foreach (var account in configuration.GetSection("BootstrapUsers").GetChildren())
        {
            var email = account["Email"];
            var password = account["Password"];
            var role = account["Role"];
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(role) || !Roles.Contains(role, StringComparer.Ordinal))
                continue;

            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                int? instructorId = int.TryParse(account["MaGiangVien"], out var parsedId) ? parsedId : null;
                user = new NguoiDungUngDung { UserName = email, Email = email, MaGiangVien = instructorId, EmailConfirmed = true };
                var created = await userManager.CreateAsync(user, password);
                if (!created.Succeeded)
                    throw new InvalidOperationException($"Could not create bootstrap account {email}: {string.Join("; ", created.Errors.Select(e => e.Description))}");
            }

            if (!await userManager.IsInRoleAsync(user, role))
            {
                var assigned = await userManager.AddToRoleAsync(user, role);
                if (!assigned.Succeeded)
                    throw new InvalidOperationException($"Could not assign role {role} to {email}: {string.Join("; ", assigned.Errors.Select(e => e.Description))}");
            }
        }
    }
}
