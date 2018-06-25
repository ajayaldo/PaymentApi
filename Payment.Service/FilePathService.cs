using System;
using System.Configuration;
using System.IO;
using Payment.Common.Exception;
using Payment.Common.ServiceInerfaces;

namespace Payment.Service
{
  public class FilePathService
     : IFilePathService
  {
    public string GetPaymentDataFilePath()
    {
      var path = ConfigurationManager.AppSettings["TextRepoFilePath"];

      if (path == null)
      {
        throw new ConfigurationMissingException("Please maintain a valid TextRepoFilePath in App config");
      }
      return path;
    }
  }
}
