using Payment.Common.Entities;
using Payment.Common.Models;
using Payment.Common.ServiceInerfaces;
using Payment.Common.RepositoryInterfaces;

namespace Payment.Service
{
  public sealed class PaymentService
    : IPaymentService
  {
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
      _paymentRepository = paymentRepository;
    }

    public string Submit(BankAccount account, DepositDetail depositDetail)
    {
      var entity = new PaymentDataEntity
      {
        AccountName = account.AccountName,
        AccountNumber = account.AccountNumber,
        Bsb = account.Bsb,
        Amount = depositDetail.Amount,
        Reference = depositDetail.Reference
      };

      return _paymentRepository.SubmitPayment(entity);
    }
  }
}
