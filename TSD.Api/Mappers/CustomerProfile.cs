using AutoMapper;
using TSD.Api.Models;
using TSD.Domain.Commands.Requests.Customers;
using TSD.Domain.Queries.Responses.Customers;

namespace TSD.Api.Mappers;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        // From DTO to Domain
        CreateMap<CustomerDto, AddCustomerCommandRequest>();

        // From Domain to DTO
        CreateMap<CustomerQueryResponse, CustomerDto>();
    }
}