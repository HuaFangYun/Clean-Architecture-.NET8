using CA.Domain.Repositories;
using CA.Persistence.Contexts;
using CA.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Persistence.Extensions
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, 
            string connection, 
            string migrationAssembly = "")
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection, sql =>
            {
                if (!string.IsNullOrEmpty(migrationAssembly))
                {
                    sql.MigrationsAssembly(migrationAssembly);
                }
            }))
            .AddDbContextFactory<ApplicationDbContext>((Action<DbContextOptionsBuilder>) null, ServiceLifetime.Scoped)
            .AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            /*services.AddScoped<ITestRepository, TestRepository>();*/
           
            services.AddScoped(typeof(IUnitOfWork), services =>
            {
                return services.GetRequiredService<ApplicationDbContext>();
            });

            return services;
        }
    }
}
