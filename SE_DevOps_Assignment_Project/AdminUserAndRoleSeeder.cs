using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SE_DevOps_DataLayer;
using System;
using System.Threading.Tasks;

namespace SE_DevOps_Assignment_Project
{
    public class AdminUserAndRoleSeeder
    {
        public static async Task SeedAdminUserAndRoleAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var userManager = scopedServices.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                var roleName = "Admin";
                var adminEmail = Environment.GetEnvironmentVariable("ADMIN_USER");
                var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

                var dbContext = scopedServices.GetRequiredService<AppDbContext>();
                await dbContext.Database.MigrateAsync();

                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(adminUser, adminPassword);
                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
        }
    }
}
