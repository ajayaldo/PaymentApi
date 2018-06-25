using System.Diagnostics;
using System.Web.Http.ExceptionHandling;
using Payment.Common.Models;
using Payment.Common.ServiceInerfaces;

namespace Payment.Api.Logger
{
  public class GlobalExceptionLogger
     : ExceptionLogger
  {
    private readonly ILoggerService _loggerService;

    public GlobalExceptionLogger(ILoggerService loggerService)
    {
      _loggerService = loggerService;
    }

    public override void Log(ExceptionLoggerContext context)
    {
      _loggerService.Log(LogLevel.Error, context.ExceptionContext.Exception.ToString());
    }
  }
}