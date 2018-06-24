using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;

namespace Payment.Api
{
  public class IocConfig
  {
    public static void Configure()
    {
      var builder = new ContainerBuilder();
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

      var container = builder.Build();
      var dependencyResolver = new AutofacWebApiDependencyResolver(container);
      GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
    }
  }
}
