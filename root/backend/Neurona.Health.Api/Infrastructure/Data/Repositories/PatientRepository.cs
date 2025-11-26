using Microsoft.EntityFrameworkCore;
using Neurona.Health.Api.Domain;
using Neurona.Health.Api.Domain.Entities;

namespace Neurona.Health.Api.Infrastructure.Data.Repositories;

// Infrastructure/Repositories/PatientRepository.cs
public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _db;

    /// <inherit/>
    public PatientRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<Patient>> GetAllAsync(CancellationToken ct = default)
        => await _db.Patients.AsNoTracking().ToListAsync(ct);

    public Task<Patient?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.Patients
            .Include(p => p.DiagnosticEntries)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<Patient> AddAsync(Patient patient, CancellationToken ct = default)
    {
        _db.Patients.Add(patient);
        await _db.SaveChangesAsync(ct);
        return patient;
    }

    public async Task UpdateAsync(Patient patient, CancellationToken ct = default)
    {
        await _db.SaveChangesAsync(ct);
    }
}
