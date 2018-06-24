using System.Diagnostics;
using System.Web.Http.ExceptionHandling;

namespace Payment.Api.Logger
{
  public class GlobalExceptionLogger
     : ExceptionLogger
  {
    public override void Log(ExceptionLoggerContext context)
    {
      Trace.TraceError(context.ExceptionContext.Exception.ToString());
    }
  }
}