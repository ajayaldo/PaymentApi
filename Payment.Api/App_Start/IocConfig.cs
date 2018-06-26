using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using Payment.Common.Models;
using Payment.Common.RepositoryInterfaces;
using Payment.Common.ServiceInterfaces;
using Payment.Repository;
using Payment.Service;

namespace Payment.Api
{
  public class IocConfig
  {
    public static void Configure()
    {
      var builder = new ContainerBuilder();
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

      builder.RegisterType<PaymentService>().As<IPaymentService>();
      builder.RegisterType<TextPaymentRepository>().As<IPaymentRepository>();
      builder.RegisterType<FilePathService>().As<IFilePathService>();
      builder.RegisterType<LoggerService>().As<ILoggerService>().SingleInstance();

      var container = builder.Build();
      var dependencyResolver = new AutofacWebApiDependencyResolver(container);
      GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
    }
  }
}
