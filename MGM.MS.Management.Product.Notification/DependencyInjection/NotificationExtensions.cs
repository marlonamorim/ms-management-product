using MGM.MS.Management.Product.Notification.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MGM.MS.Management.Product.Notification.DependencyInjection
{
    public static class NotificationExtensions
    {
        public static IServiceCollection AddNotification(this IServiceCollection services)
        {
            services.AddScoped<INotification, Services.Notification>();

            return services;
        }
    }
}
