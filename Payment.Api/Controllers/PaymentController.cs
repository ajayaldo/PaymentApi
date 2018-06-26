using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Payment.Api.DTOs;
using Payment.Api.Filters;
using Payment.Common.Models;
using Payment.Common.ServiceInterfaces;

namespace Payment.Api.Controllers
{
  [EnableCors("*", "*", "*")]
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
    [Route("payment")]
    public IHttpActionResult Post([FromBody] PaymentDataDto paymentDataDto)
    {
      _loggerService.Log(LogLevel.Info, "Submit payment start.");

      var depositDetail = Mapper.Map<PaymentDataDto, DepositDetail>(paymentDataDto);
      var account = Mapper.Map<PaymentDataDto, BankAccount>(paymentDataDto);

      var transactionId = _paymentService.Submit(account, depositDetail);

      _loggerService.Log(LogLevel.Info, "Submit payment successful.");

      return Created($"{Request.RequestUri}/{transactionId}", paymentDataDto);
    }
  }
}