using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Domain.Roles;

namespace ToDoList.Services.Seeder;

public class RoleSeeder
{
    public static async Task SeedAsync(WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            if (_roleManager == null)
                throw new Exception("Role manager is not available");

            var roles = new List<string>()
            {
                ApiUserRoles.User,
                ApiUserRoles.Admin,
                GroupMemberRoles.Member,
                GroupMemberRoles.Admin,
            };

            foreach (var roleName in roles)
            {
                if (!(await _roleManager.RoleExistsAsync(roleName)))
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
