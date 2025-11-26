using Neurona.Health.Api.Application.Patients;
using Neurona.Health.Api.Domain;

namespace Neurona.Health.Api.Application;

// Application/DependencyInjection.cs
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPatientService, PatientService>();
        return services;
    }
}
