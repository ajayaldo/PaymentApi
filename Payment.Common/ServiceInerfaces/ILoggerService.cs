using Payment.Common.Models;

namespace Payment.Common.ServiceInerfaces
{
  public interface ILoggerService
  {
    void Log(LogLevel logLevel,string log);
  }
}
