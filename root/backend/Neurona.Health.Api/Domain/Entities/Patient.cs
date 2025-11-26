using System.ComponentModel.DataAnnotations;
using Neurona.Health.Api.Domain.Commons;

namespace Neurona.Health.Api.Domain.Entities;

public class Patient: BaseIdentity
{
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    [MaxLength(500)]
    public string? LastDiagnosis { get; set; } = null;
    public List<DiagnosticEntry> DiagnosticEntries { get; set; } = new();
}