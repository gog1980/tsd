namespace TSD.Domain.Queries.Responses.Customers;

public record CustomerQueryResponse : BaseEntityQueryResponse
{
    public required string FullName { get; set; }

    public int ExternalId { get; set; }
}