using Bookstore.Server.Models;
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
        public AppDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }
        public DbSet<BookModel>? Books { get; set; }
        public DbSet<AuthorModel>? Authors { get; set; }
        public DbSet<PublishingHouseModel>? PublishingHouses { get; set; }
        public DbSet<CatalogModel>? Catalogs { get; set; }
    }
}
