﻿using Bookstore.Server.Core;
using Bookstore.Shared.DbModels;

namespace Bookstore.Server.Helper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBookstoreService(this IServiceCollection services)
        {
            services.AddTransient<AuthorCore>();
            services.AddTransient<BookCore>();
            services.AddTransient<CartCore>();
            services.AddTransient<CatalogCore>();
            services.AddTransient<PublishingHouseCore>();

            return services;
        }
    }
}
