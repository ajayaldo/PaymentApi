using System.Web.Http;
using System.Web.Http.Tracing;
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
    private readonly ILoggerService _loggerService;

    public PaymentController(IPaymentService paymentService, ILoggerService loggerService)
    {
      _paymentService = paymentService;
      _loggerService = loggerService;
    }

    [HttpPost]
    [ValidateDto]
    [Route("submit")]
    public IHttpActionResult Post([FromBody] PaymentDataDto paymentDataDto)
    {
      Configuration.Services.GetTraceWriter().Info(Request, "PaymentController", "Payment submit");

      var depositDetail = Mapper.Map<PaymentDataDto, DepositDetail>(paymentDataDto);
      var account = Mapper.Map<PaymentDataDto, BankAccount>(paymentDataDto);

      var transactionId = _paymentService.Submit(account, depositDetail);

      return Created(transactionId, paymentDataDto);
    }
  }
}