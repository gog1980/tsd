using Microsoft.EntityFrameworkCore;
using TSD.Domain.Entities;

namespace TSD.Infrastructure;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
}