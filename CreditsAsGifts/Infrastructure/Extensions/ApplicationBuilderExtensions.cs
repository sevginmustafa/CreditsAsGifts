namespace CreditsAsGifts.Infrastructure.Extensions
{
    using CreditsAsGifts.Data;
    using CreditsAsGifts.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
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
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await MigrateDatabaseAsync(database);

            await SeedRoleAsync(roleManager, AdministratorRoleName);
            await SeedRoleAsync(roleManager, UserRoleName);
            await SeedAdministratorAsync(userManager);
            await SeedPrivacyAsync(database);
            await SeedTermsAndConditionsAsync(database);

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

        private static async Task SeedAdministratorAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any(x => x.UserName == AdministratorUsername))
            {
                var user = new ApplicationUser
                {
                    FirstName = AdministratorFirstName,
                    LastName = AdministratorLastName,
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

        private static async Task SeedPrivacyAsync(CreditsAsGiftsDbContext database)
        {
            const string privacyPath = "./wwwroot/privacy.txt";

            if (!database.Privacies.Any())
            {
                var privacy = new Privacy
                {
                    Content = await File.ReadAllTextAsync(privacyPath),
                };

                await database.Privacies.AddAsync(privacy);

                await database.SaveChangesAsync();
            }
        }

        private static async Task SeedTermsAndConditionsAsync(CreditsAsGiftsDbContext database)
        {
            const string privacyPath = "./wwwroot/termsAndConditions.txt";

            if (!database.TermsAndConditions.Any())
            {
                var termsAndConditions = new TermsAndConditions
                {
                    Content = await File.ReadAllTextAsync(privacyPath),
                };

                await database.TermsAndConditions.AddAsync(termsAndConditions);

                await database.SaveChangesAsync();
            }
        }
    }
}
