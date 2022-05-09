namespace CreditsAsGifts.Infrastructure.Extensions
{
    using CreditsAsGifts.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static CreditsAsGifts.Common.GlobalConstants;

    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(
               this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateAsyncScope();
            var services = serviceScope.ServiceProvider;

            var database = services.GetRequiredService<CreditsAsGiftsDbContext>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await MigrateDatabaseAsync(database);

            await SeedRoleAsync(roleManager, AdministratorRoleName);
            await SeedRoleAsync(roleManager, UserRoleName);
            await SeedAdministratorAsync(userManager);

            return app;
        }

        private static async Task MigrateDatabaseAsync(CreditsAsGiftsDbContext database)
        {
            await database.Database.MigrateAsync();
        }

        private static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedAdministratorAsync(UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any(x => x.UserName == AdministratorUsername))
            {
                var user = new IdentityUser
                {
                    UserName = AdministratorUsername,
                    Email = AdministratorEmail,
                    PhoneNumber = AdministratorPhoneNumber
                };

                var result = await userManager.CreateAsync(user, AdministratorPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, AdministratorRoleName);
                }
                else
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
