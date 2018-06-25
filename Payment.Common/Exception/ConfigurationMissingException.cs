using System.Configuration;

namespace Payment.Common.Exception
{
  public class ConfigurationMissingException
    : ConfigurationErrorsException
  {
    public ConfigurationMissingException(string missingkey)
    : base(missingkey)
    { }
  }
}
