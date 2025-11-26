using Neurona.Health.Api.Application;

namespace Neurona.Health.Api.Api.GraphQL.Types;

public record PatientDetailOutput(
    PatientDto Patient,
    IReadOnlyList<DiagnosticEntryDto> Diagnostics
);