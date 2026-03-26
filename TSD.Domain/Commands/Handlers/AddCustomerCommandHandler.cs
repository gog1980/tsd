using MediatR;
using TSD.Domain.Commands.Requests.Customers;
using TSD.Domain.Entities;
using TSD.Domain.Repositories;

namespace TSD.Domain.Commands.Handlers;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommandRequest, Guid>
{
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Guid> Handle(AddCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        // 1. Cross-Entity Validation (Database-dependent)
        var isUnique = await _customerRepository.IsExternalIdUnique(request.ExternalId);
        if (!isUnique)
        {
            throw new InvalidOperationException("This External ID is already assigned to another customer.");
        }

        // 2. Entity Creation (Internal Validation)
        var customer = Customer.Create(request);
        if (customer.IsFailure)
        {
            throw new InvalidOperationException(customer.Error);
        }

        // 3. Persistence
        await _customerRepository.AddAsync(customer.Value);
        await _customerRepository.SaveChangesAsync();

        return customer.Value.Id;
    }
}