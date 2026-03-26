using AutoMapper;
using TSD.Domain.Entities;
using TSD.Domain.Queries.Responses.Customers;

namespace TSD.Domain.Mappers;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerQueryResponse>();
    }
}