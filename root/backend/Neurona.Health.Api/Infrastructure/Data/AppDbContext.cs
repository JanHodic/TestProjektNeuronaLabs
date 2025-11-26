using Microsoft.EntityFrameworkCore;
using Neurona.Health.Api.Domain.Entities;

namespace Neurona.Health.Api.Infrastructure.Data;

public class AppDbContext: DbContext
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<DiagnosticEntry> DiagnosticEntries => Set<DiagnosticEntry>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
            .HasMany(p => p.DiagnosticEntries)
            .WithOne(d => d.Patient)
            .HasForeignKey(d => d.PatientId);
    }
}