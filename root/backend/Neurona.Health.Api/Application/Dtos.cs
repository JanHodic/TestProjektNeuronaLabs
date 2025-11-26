namespace Neurona.Health.Api.Application;

// DTOs
public record CreatePatientInput(string FullName, int Age, string? LastDiagnosis);
public record UpdateDiagnosticInput(Guid PatientId, string Diagnosis, string? Notes);

public record PatientDto(
    Guid Id,
    string FullName,
    int Age,
    string? LastDiagnosis
);

public record DiagnosticEntryDto(
    Guid Id,
    DateTime RecordedAt,
    string Diagnosis,
    string? Notes
);
