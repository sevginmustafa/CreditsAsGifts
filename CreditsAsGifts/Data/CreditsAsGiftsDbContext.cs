using CreditsAsGifts.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CreditsAsGifts.Data
{
    public class CreditsAsGiftsDbContext : IdentityDbContext
    {
        public CreditsAsGiftsDbContext(DbContextOptions<CreditsAsGiftsDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<GiftReceived> GiftsReceived { get; set; }

        public DbSet<GiftSended> GiftsSended { get; set; }

        public DbSet<Privacy> Privacies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var entityTypes = builder.Model.GetEntityTypes().ToList();

            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys()
                .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
    }
}
