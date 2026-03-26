using MediatR;
using TSD.Domain.Queries.Responses.Customers;

namespace TSD.Domain.Queries.Requests.Customers;

public record GetCustomerByIdQueryRequest(Guid Id) : IRequest<CustomerQueryResponse>;