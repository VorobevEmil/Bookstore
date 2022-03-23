using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;

namespace Bookstore.Server.Helper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBookstoreService(this IServiceCollection services)
        {
            services.AddTransient<BookstoreService<PublishingHouseModel>>();
            services.AddTransient<BookstoreService<AuthorModel>>();
            services.AddTransient<BookstoreService<CatalogModel>>();
            services.AddTransient<BookstoreService<BookModel>>();

            return services;
        }

        public static IServiceCollection AddCustomModelService(this IServiceCollection services)
        {
            services.AddTransient<BookModelService>();
            services.AddTransient<AuthorModelService>();

            return services;
        }
    }
}
