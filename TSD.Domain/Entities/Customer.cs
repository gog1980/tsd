using CSharpFunctionalExtensions;
using TSD.Domain.Commands.Requests.Customers;

namespace TSD.Domain.Entities;

public class Customer : BaseEntity
{
    public string FullName { get; private set; } = string.Empty;

    public int ExternalId { get; private set; }

    public static Result<Customer> Create(AddCustomerCommandRequest request)
    {
        var customer = new Customer();

        var results = new List<Result>()
        {
            customer.SetFullName(request.FullName),
            customer.SetExternalId(request.ExternalId)
        }.Combine(", ");

        if (results.IsFailure)
        {
            return Result.Failure<Customer>(results.Error);
        }

        return Result.Success(customer);
    }

    private Result SetFullName(string? fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Result.Failure("Full name cannot be empty.");
        }
        FullName = fullName;
        return Result.Success();
    }

    private Result SetExternalId(int externalId)
    {
        if (externalId <= 0)
        {
            return Result.Failure("External ID must be a positive integer.");
        }
        ExternalId = externalId;
        return Result.Success();
    }
}