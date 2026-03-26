namespace TSD.Domain.Queries.Responses;

public abstract record BaseEntityQueryResponse
{
    public Guid Id { get; set; }
}