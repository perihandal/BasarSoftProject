using App.Repositories.Interfaces;
using App.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace App.Repositories.RepositoryExtentions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringOption = configuration.GetSection(ConnectionStringOption.Key)
                                                      .Get<ConnectionStringOption>();

            if (connectionStringOption == null || string.IsNullOrEmpty(connectionStringOption.PostgresConnection))
            {
                throw new InvalidOperationException("Postgres bağlantı dizesi yapılandırılmamış.");
            }

            // PostgreSQL + NetTopologySuite (PostGIS) konfigurasyonu
            services.AddDbContext<EFAppDbContext>(options =>
                options.UseNpgsql(
                    connectionStringOption.PostgresConnection,
                    npgsqlOptions => {
                        npgsqlOptions.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                        npgsqlOptions.UseNetTopologySuite(); // PostGIS geometriler için
                }));

            // Repository ve UnitOfWork DI
            services.AddScoped<IGeometryRepository, GeometryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
