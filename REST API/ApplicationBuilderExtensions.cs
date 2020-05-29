using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace REST_API
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder services, string connectionString)
            where T : REST_API.SQLDependency.Interface.IDatabaseSubscription
        {
            var serviceProvider = services.ApplicationServices;
            var subscription = serviceProvider.GetService<T>();
            subscription.Configure(connectionString);
        }
    }
}
