using Bookstore.Shared.DbModels;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bookstore.Server.Data
{
    public class AppDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }
#nullable disable
        public DbSet<BookModel> Books { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<PublishingHouseModel> PublishingHouses { get; set; }
        public DbSet<CatalogModel> Catalogs { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<BookModelOrderModel> BookModelOrderModel { get; set; }
        public DbSet<FeedbackModel> Feedbacks { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithOne(p => p.Profile)
                    .HasForeignKey<UserProfile>(d => d.IdUser);

            });

            base.OnModelCreating(builder);
        }
    }
}
