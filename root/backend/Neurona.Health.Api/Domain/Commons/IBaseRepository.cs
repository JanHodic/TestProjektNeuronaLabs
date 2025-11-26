using Neurona.Health.Api.Domain.Commons;

namespace Neurona.Health.Api.Entities.Commons;

public interface IBaseRepository<T>
where T : BaseIdentity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<T> AddAsync(T patient, CancellationToken ct = default);
    Task UpdateAsync(T patient, CancellationToken ct = default);
}