using Neurona.Health.Api.Application;
using Neurona.Health.Api.Application.Patients.Commands;

namespace Neurona.Health.Api.Domain;

// Application/Patients/IPatientService.cs
public interface IPatientService
{
    Task<IReadOnlyList<PatientDto>> GetPatientsAsync();
    Task<(PatientDto Patient, IReadOnlyList<DiagnosticEntryDto> Diagnostics)?> GetPatientDetailAsync(Guid id);
    Task<PatientDto> CreatePatientAsync(CreatePatientCommand cmd);
    Task<PatientDto?> AddDiagnosticEntryAsync(AddDiagnosticEntryCommand cmd);
}
