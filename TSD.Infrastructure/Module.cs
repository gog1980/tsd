using Microsoft.Extensions.DependencyInjection;
using TSD.Domain.Repositories;
using TSD.Infrastructure.Repositories;

namespace TSD.Infrastructure;

public static class Module
{
    public static void RegisterServices(IServiceCollection services)
    {
        RegisterRepositories(services);
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }
}