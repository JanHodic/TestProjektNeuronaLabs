using Neurona.Health.Api.Domain.Commons;
using Neurona.Health.Api.Entities;
using Neurona.Health.Api.Entities.Commons;

namespace Neurona.Health.Api.Domain.Entities;

/// <summary>
/// One diagnosis of given patient
/// </summary>
public class DiagnosticEntry: BaseIdentity
{
    public DateTime RecordedAt { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string? Notes { get; set; }

    public Guid PatientId { get; set; }
    public Patient? Patient { get; set; } = null;
}