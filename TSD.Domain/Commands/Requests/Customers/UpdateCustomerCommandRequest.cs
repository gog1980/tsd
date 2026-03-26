namespace TSD.Domain.Commands.Requests.Customers;

public class UpdateCustomerCommandRequest : SaveCustomerCommandRequest
{
    public Guid Id { get; set; }

    public UpdateCustomerCommandRequest(Guid id)
    {
        Id = id;
    }
}