using Neurona.Health.Api.Application;
using Neurona.Health.Api.Application.Patients.Commands;
using Neurona.Health.Api.Domain;

namespace Neurona.Health.Api.Api.GraphQL.Mutations;

public class Mutation
{
    private readonly IPatientService _patientService;

    public Mutation(IPatientService patientService)
    {
        _patientService = patientService;
    }

    public Task<PatientDto> CreatePatient(CreatePatientCommand input)
        => _patientService.CreatePatientAsync(input);

    public Task<PatientDto?> AddDiagnosticEntry(AddDiagnosticEntryCommand input)
        => _patientService.AddDiagnosticEntryAsync(input);
}
