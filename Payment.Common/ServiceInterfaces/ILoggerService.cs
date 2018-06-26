using Payment.Common.Models;

namespace Payment.Common.ServiceInterfaces
{
  public interface ILoggerService
  {
    void Log(LogLevel logLevel,string log);
  }
}
