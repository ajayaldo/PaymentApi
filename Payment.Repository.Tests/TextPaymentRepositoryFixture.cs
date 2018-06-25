using System;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Payment.Common.Entities;
using Payment.Common.ServiceInerfaces;

namespace Payment.Repository.Tests
{
  public class TextPaymentRepositoryFixture
  {
    private readonly IFilePathService _fakeFilePathService;
    private readonly TextPaymentRepository _repository;
    private readonly PaymentDataEntity _submitPaymentEntity;

    public TextPaymentRepositoryFixture()
    {
      var fixture = new Fixture();
      _fakeFilePathService = A.Fake<IFilePathService>();
      _repository = new TextPaymentRepository(_fakeFilePathService);
      _submitPaymentEntity = fixture.Create<PaymentDataEntity>();
      var tempPath = AppDomain.CurrentDomain.BaseDirectory + "PaymentData.txt";
      A.CallTo(() => _fakeFilePathService.GetPaymentDataFilePath()).Returns(tempPath);
    }

    [Test]
    public void Should_Call_GetPaymentDataSubmitPath()
    {
      _repository.SubmitPayment(_submitPaymentEntity);

      A.CallTo(() => _fakeFilePathService.GetPaymentDataFilePath()).MustHaveHappened();
    }

    [Test]
    public void Should_Call_Expected_ResultType()
    {
      var result = _repository.SubmitPayment(_submitPaymentEntity);
      result.Should().BeOfType<string>();
    }
  }
}
