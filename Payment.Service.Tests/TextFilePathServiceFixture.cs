using System.Configuration;
using FluentAssertions;
using NUnit.Framework;

namespace Payment.Service.Tests
{
  public sealed class TextFilePathServiceFixture
  {

    [Test]
    public void Should_Provide_Path_From_Configuration()
    {
      var service = new FilePathService();
      var expectedPath = ConfigurationManager.AppSettings["TextRepoFilePath"];
      var result = service.GetPaymentDataFilePath();

      result.Should().Be(expectedPath);
    }
  }
}
