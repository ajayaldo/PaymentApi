﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using AutoFixture;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using NUnit.Framework;
using Payment.Api.Controllers;
using Payment.Api.DTOs;
using Payment.Common.Models;
using Payment.Common.ServiceInterfaces;

namespace Payment.Api.Tests
{
  public class PaymentControllerFixture
  {
    private readonly IPaymentService _fakePaymentService;
    private readonly PaymentDataDto _paymentDataDto;
    private readonly PaymentController _controller;
    private readonly Fixture _fixture;
    private readonly ILoggerService _fakeLoggerService;
    private readonly string _paymentUri;

    public PaymentControllerFixture()
    {
      _fixture = new Fixture();
      _fakePaymentService = A.Fake<IPaymentService>();
      _fakeLoggerService = A.Fake<ILoggerService>();
      _paymentDataDto = _fixture.Create<PaymentDataDto>();
      var _paymentUri = "http://localhost/payment";

      _controller =
        new PaymentController(_fakePaymentService, _fakeLoggerService)
        {
          Request = new HttpRequestMessage { RequestUri = new Uri(_paymentUri) }
        };


      var transaction = _fixture.Create<string>();
      A.CallTo(() => _fakePaymentService.Submit(A<BankAccount>.Ignored, A<DepositDetail>.Ignored)).Returns(transaction);

      Mapper.Initialize(config =>
      {
        config.CreateMap<PaymentDataDto, DepositDetail>();
        config.CreateMap<PaymentDataDto, BankAccount>();
      });
    }

    [Test]
    public void Post_Result_Should_Not_Be_Null()
    {
      var httpActionResult = _controller.Post(_paymentDataDto);

      httpActionResult.Should().NotBeNull();
    }

    [Test]
    public void Post_Return_Type_Should_Be_Of_Type_CreatedNegotiatedResult()
    {
      var httpActionResult = _controller.Post(_paymentDataDto);

      httpActionResult.Should().BeOfType<CreatedNegotiatedContentResult<PaymentDataDto>>();
    }

    [Test]
    public void Post_Result_Content_Should_Be_As_Expected()
    {
      var httpActionResult = _controller.Post(_paymentDataDto);
      var contentResult = httpActionResult as CreatedNegotiatedContentResult<PaymentDataDto>;
      contentResult.Content.IsSameOrEqualTo(_paymentDataDto);
    }

    [Test]
    public void Post_Content_Returned_Should_Not_Be_Null()
    {
      var httpActionResult = _controller.Post(_paymentDataDto);
      var createdResult = httpActionResult as CreatedNegotiatedContentResult<PaymentDataDto>;

      Assert.IsNotNull(createdResult.Content);
    }

    [Test]
    public void Post_Should_Call_Submit_PaymentService_Method()
    {
      _controller.Post(_paymentDataDto);

      A.CallTo(() => _fakePaymentService.Submit(A<BankAccount>.Ignored, A<DepositDetail>.Ignored)).MustHaveHappened();
    }

    [Test]
    public void Post_Should_Call_SubmitPayment_Service_With_BankDetail()
    {
      BankAccount capturedBankAccount = null;
      DepositDetail capturedDepositDetail = null;

      A.CallTo(() => _fakePaymentService.Submit(A<BankAccount>.Ignored, A<DepositDetail>.Ignored))
        .Invokes((BankAccount ba, DepositDetail da) =>
          {
            capturedBankAccount = ba;
            capturedDepositDetail = da;
          });

      var account = Mapper.Map<PaymentDataDto, BankAccount>(_paymentDataDto);
      var depositDetail = Mapper.Map<PaymentDataDto, DepositDetail>(_paymentDataDto);

      _controller.Post(_paymentDataDto);

      capturedBankAccount.Should().BeEquivalentTo(account);
      capturedDepositDetail.Should().BeEquivalentTo(depositDetail);
    }
  }
}
