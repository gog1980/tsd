using MediatR;

namespace TSD.Domain.Commands.Requests.Customers;

public abstract class SaveCustomerCommandRequest : IRequest<Guid>
{
    public string? FullName { get; set; }

    public int ExternalId { get; set; }
}