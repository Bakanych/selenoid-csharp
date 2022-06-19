using Autofac;

namespace Web;

public static class DependencySetup
{
    public static void RegisterDependencies()
    {
        var builder = new ContainerBuilder();
        // builder.RegisterType<WebApplication>().As<IWebApplication>();
        // builder.RegisterType<WebApplication>().As<IWebApplication>();
    }
}