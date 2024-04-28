using CA.CrossCuttingConcerns.Utilities;
using CA.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddDateTimeProvider();
        }

        public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
