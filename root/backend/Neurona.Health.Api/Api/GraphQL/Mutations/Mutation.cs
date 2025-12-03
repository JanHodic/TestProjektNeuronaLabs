using Neurona.Health.Api.Application;
using Neurona.Health.Api.Application.Patients.Commands;
using Neurona.Health.Api.Domain;
using HotChocolate.Authorization;

namespace Neurona.Health.Api.Api.GraphQL.Mutations;

public class Mutation
{
    [Authorize]
    public Task<PatientDto> CreatePatient(CreatePatientCommand input, [Service] IPatientService patientService)
        => patientService.CreatePatientAsync(input);

    [Authorize]
    public Task<PatientDto?> AddDiagnosticEntry(AddDiagnosticEntryCommand input, [Service] IPatientService patientService)
        => patientService.AddDiagnosticEntryAsync(input);
}
