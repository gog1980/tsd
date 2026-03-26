using TSD.Domain.Entities;

namespace TSD.Domain.Repositories;

public interface ICustomerRepository
{
    Task<bool> IsExternalIdUnique(int externalId);

    Task AddAsync(Customer customer);

    Task SaveChangesAsync();

    Task<Customer?> GetByIdAsync(Guid id);
}