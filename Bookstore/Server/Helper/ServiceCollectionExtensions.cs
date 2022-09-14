using Bookstore.Server.Service;
using Bookstore.Shared.DbModels;

namespace Bookstore.Server.Helper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBookstoreService(this IServiceCollection services)
        {
            services.AddTransient<AuthorService>();
            services.AddTransient<BookService>();
            services.AddTransient<CartService>();
            services.AddTransient<CatalogService>();
            services.AddTransient<PublishingHouseService>();
            services.AddTransient<OrderService>();
            services.AddTransient<BookOrderCore>();
            services.AddTransient<FeedbackService>();
            return services;
        }
    }
}
