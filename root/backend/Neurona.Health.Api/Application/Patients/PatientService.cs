using Neurona.Health.Api.Application.Patients.Commands;
using Neurona.Health.Api.Domain;
using Neurona.Health.Api.Domain.Entities;

namespace Neurona.Health.Api.Application.Patients;

// Application/Patients/PatientService.cs
public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;

    /// <inherit/>
    public PatientService(IPatientRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<PatientDto>> GetPatientsAsync()
    {
        var patients = await _repo.GetAllAsync();
        return patients
            .Select(p => new PatientDto(p.Id, p.FullName, p.Age, p.LastDiagnosis))
            .ToList();
    }

    public async Task<(PatientDto, IReadOnlyList<DiagnosticEntryDto>)?> GetPatientDetailAsync(Guid id)
    {
        var patient = await _repo.GetByIdAsync(id);
        if (patient is null) return null;

        var dto = new PatientDto(patient.Id, patient.FullName, patient.Age, patient.LastDiagnosis);
        var diagnostics = patient.DiagnosticEntries
            .OrderBy(d => d.RecordedAt)
            .Select(d => new DiagnosticEntryDto(d.Id, d.RecordedAt, d.Diagnosis, d.Notes))
            .ToList();

        return (dto, diagnostics);
    }

    public async Task<PatientDto> CreatePatientAsync(CreatePatientCommand cmd)
    {
        var patient = new Patient
        {
            FullName = cmd.FullName,
            Age = cmd.Age,
            LastDiagnosis = cmd.LastDiagnosis
        };

        patient = await _repo.AddAsync(patient);
        return new PatientDto(patient.Id, patient.FullName, patient.Age, patient.LastDiagnosis);
    }

    public async Task<PatientDto?> AddDiagnosticEntryAsync(AddDiagnosticEntryCommand cmd)
    {
        var patient = await _repo.GetByIdAsync(cmd.PatientId);
        if (patient is null) return null;

        patient.DiagnosticEntries.Add(new DiagnosticEntry
        {
            RecordedAt = DateTime.UtcNow,
            Diagnosis = cmd.Diagnosis,
            Notes = cmd.Notes,
            PatientId = patient.Id
        });

        patient.LastDiagnosis = cmd.Diagnosis;
        await _repo.UpdateAsync(patient);

        return new PatientDto(patient.Id, patient.FullName, patient.Age, patient.LastDiagnosis);
    }
}