using AutoMapper;
using MediatR;
using TSD.Domain.Queries.Requests.Customers;
using TSD.Domain.Queries.Responses.Customers;
using TSD.Domain.Repositories;

namespace TSD.Domain.Queries.Handlers.Customers;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQueryRequest, CustomerQueryResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerQueryResponse> Handle(GetCustomerByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer == null) return null!;

        return _mapper.Map<CustomerQueryResponse>(customer);
    }
}