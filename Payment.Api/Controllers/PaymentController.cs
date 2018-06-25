using System.Web.Http;
using AutoMapper;
using Payment.Api.DTOs;
using Payment.Api.Filters;
using Payment.Common.Models;
using Payment.Common.ServiceInerfaces;

namespace Payment.Api.Controllers
{
  public class PaymentController
    : ApiController
  {
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
      _paymentService = paymentService;
    }

    [HttpPost]
    [ValidateDto]
    [Route("submit")]
    public IHttpActionResult Post([FromBody] PaymentDataDto paymentDataDto)
    {
      var depositDetail = Mapper.Map<PaymentDataDto, DepositDetail>(paymentDataDto);
      var account = Mapper.Map<PaymentDataDto, BankAccount>(paymentDataDto);

      var transactionId = _paymentService.Submit(account, depositDetail);

      return Created(transactionId, paymentDataDto);
    }
  }
}