using System.Reflection;
using DomainModule = TSD.Domain.Module;

namespace TSD.Api;

public static class Module
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }

    public static Assembly GetDomainAssembly()
    {
        return DomainModule.GetAssembly();
    }
}