using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Application.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ApplicationServiceExtension).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceExtension).Assembly));

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            return services;
        }
    }
}
