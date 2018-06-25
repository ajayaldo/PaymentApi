using Payment.Api.Logger;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Tracing;

namespace Payment.Api
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
      var traceWriter = config.EnableSystemDiagnosticsTracing();
      traceWriter.IsVerbose = true;
      traceWriter.MinimumLevel = TraceLevel.Debug;

      config.MapHttpAttributeRoutes();
    }
  }
}
