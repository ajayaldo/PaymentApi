using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Payment.Common.Entities;
using Payment.Common.Models;
using Payment.Common.RepositoryInterfaces;

namespace Payment.Service.Tests
{
  class PaymentServiceFixture
  {
    private readonly IPaymentRepository _paymentRepository;
    private readonly PaymentService _submitPaymentService;
    private readonly BankAccount _account;
    private readonly DepositDetail _depositDetail;
    private readonly Fixture _fixture;

    public PaymentServiceFixture()
    {
      _fixture = new Fixture();
      _paymentRepository = A.Fake<IPaymentRepository>();
      _submitPaymentService = new PaymentService(_paymentRepository);
      _account = _fixture.Create<BankAccount>();
      _depositDetail = _fixture.Create<DepositDetail>();
    }


    [Test]
    public void Should_Call_SubmitPayment_Repository_Method()
    {
      _submitPaymentService.Submit(_account, _depositDetail);

      A.CallTo(() => _paymentRepository.SubmitPayment(A<PaymentDataEntity>.Ignored)).MustHaveHappened();
    }

    [Test]
    public void Should_Call_SubmitPayment_Repository_With_PaymentDataEntity()
    {
      var id = _fixture.Create<string>();
      A.CallTo(() => _paymentRepository.SubmitPayment(A<PaymentDataEntity>.Ignored)).Returns(id);

      PaymentDataEntity capturedObject = null;
      A.CallTo(() => _paymentRepository.SubmitPayment(A<PaymentDataEntity>.Ignored))
        .Invokes((PaymentDataEntity pd) => capturedObject = pd);

      var expectedPaymentDataEntity = new PaymentDataEntity
      {
        AccountName = _account.AccountName,
        AccountNumber = _account.AccountNumber,
        Bsb = _account.Bsb,
        Amount = _depositDetail.Amount,
        Reference = _depositDetail.Reference
      };

      _submitPaymentService.Submit(_account, _depositDetail);

      capturedObject.ShouldBeEquivalentTo(expectedPaymentDataEntity);
    }

    [Test]
    public void Should_Return_Expected_Result()
    {
      var id = _fixture.Create<string>();
      A.CallTo(() => _paymentRepository.SubmitPayment(A<PaymentDataEntity>.Ignored)).Returns(id);

      var result = _submitPaymentService.Submit(_account, _depositDetail);

      result.Should().Be(id);
    }
  }
}
