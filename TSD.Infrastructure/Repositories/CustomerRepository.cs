using Microsoft.EntityFrameworkCore;
using TSD.Domain.Entities;
using TSD.Domain.Repositories;

namespace TSD.Infrastructure.Repositories;

internal class CustomerRepository : ICustomerRepository
{
    private readonly MainContext _context;

    public CustomerRepository(MainContext context) => _context = context;

    public async Task<bool> IsExternalIdUnique(int externalId)
    {
        var isAny = await _context.Customers.AnyAsync(c => c.ExternalId == externalId);
        return !isAny;
    }

    public async Task AddAsync(Customer customer) => await _context.Customers.AddAsync(customer);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers.FindAsync(id);
    }
}