using System.Reflection;

namespace TSD.Domain;

public static class Module
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}