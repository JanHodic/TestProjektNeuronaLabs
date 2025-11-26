namespace Neurona.Health.Api.Application.Patients.Commands;

// Application/Patients/CreatePatientCommand.cs
public record CreatePatientCommand(string FullName, int Age, string? LastDiagnosis);

public record AddDiagnosticEntryCommand(Guid PatientId, string Diagnosis, string? Notes);