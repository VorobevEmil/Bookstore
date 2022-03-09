using Bookstore.Shared.DbModels;

namespace Bookstore.Server.Data.Service
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

        public static IServiceCollection AddFileManagementService(this IServiceCollection services)
        {
            services.AddTransient<FileManagementService<AuthorModel>>();
            services.AddTransient<FileManagementService<BookModel>>();

            return services;
        }

        public static IServiceCollection AddCustomModelService(this IServiceCollection services)
        {
            services.AddTransient<BookModelService>();

            return services;
        }
    }
}
