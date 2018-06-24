using Payment.Api.Logger;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Payment.Api
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
      config.MapHttpAttributeRoutes();
    }
  }
}
