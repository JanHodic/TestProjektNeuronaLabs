using Neurona.Health.Api.Application;
using Neurona.Health.Api.Application.Patients.Commands;
using Neurona.Health.Api.Domain;
using HotChocolate.Authorization;

namespace Neurona.Health.Api.Api.GraphQL.Mutations;

public class Mutation
{
    private readonly IPatientService _patientService;

    public Mutation(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [Authorize]
    public Task<PatientDto> CreatePatient(CreatePatientCommand input)
        => _patientService.CreatePatientAsync(input);

    [Authorize]
    public Task<PatientDto?> AddDiagnosticEntry(AddDiagnosticEntryCommand input)
        => _patientService.AddDiagnosticEntryAsync(input);
}
