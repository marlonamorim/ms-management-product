using MGM.MS.Management.Product.Services.Interfaces;
using MGM.MS.Management.Product.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MGM.MS.Management.Product.Services.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
