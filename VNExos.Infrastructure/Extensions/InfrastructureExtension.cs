using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VNExos.Domain.Presistence;
using VNExos.Domain.Repositories;
using VNExos.Infrastructure.Repositories;

namespace VNExos.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("VNExos-DBCS");
        Console.WriteLine("VNExos-DBCS: " + connectionString);

        services.AddDbContext<VNExosContext>(
            options => options.UseSqlServer(
                connectionString,
                b => b.MigrationsAssembly("VNExos.API")
            )
        );

        services.AddScoped<ALanguageRepository, LanguageRepository>();
    }
}
