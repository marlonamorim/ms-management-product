using MGM.MS.Management.Product.Infrastructure.Contexts;
using MGM.MS.Management.Product.Infrastructure.Interfaces;
using MGM.MS.Management.Product.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MGM.MS.Management.Product.Infrastructure.DependencyInjection
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<MongoContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
