using Neurona.Health.Api.Api.GraphQL.Types;
using Neurona.Health.Api.Application;
using Neurona.Health.Api.Domain;

namespace Neurona.Health.Api.Api.GraphQL.Queries;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1812")]
public class Query
{
    private readonly IPatientService _patientService;

    /// <inherit/>
    public Query(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public Task<IReadOnlyList<PatientDto>> GetPatients()
        => _patientService.GetPatientsAsync();

    public async Task<PatientDetailOutput?> GetPatientById(Guid id)
    {
        var result = await _patientService.GetPatientDetailAsync(id);
        if (result is null) return null;

        var patient = result.Value.Patient;
        var diagnostics = result.Value.Diagnostics;
        return new PatientDetailOutput(patient, diagnostics);
    }
}
