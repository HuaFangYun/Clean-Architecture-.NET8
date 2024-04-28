using CA.CrossCuttingConcerns.Constants;

namespace CA.WebAPI.Configurations
{
    public static class Cors
    {
        public static void AllowCors(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(ConfigurationConstants.CorsPolicy,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }
    }
}
