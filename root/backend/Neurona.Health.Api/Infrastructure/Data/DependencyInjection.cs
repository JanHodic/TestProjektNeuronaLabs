using Microsoft.EntityFrameworkCore;
using Neurona.Health.Api.Domain;
using Neurona.Health.Api.Infrastructure.Data.Repositories;

namespace Neurona.Health.Api.Infrastructure.Data;

// Infrastructure/DependencyInjection.cs
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPatientRepository, PatientRepository>();

        return services;
    }
}
