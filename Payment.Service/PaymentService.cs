using System;
using Payment.Common.Entities;
using Payment.Common.Models;
using Payment.Common.ServiceInterfaces;
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
      var id = Guid.NewGuid().ToString();

      var entity = new PaymentDataEntity
      {
        Id = id,
        AccountName = account.AccountName,
        AccountNumber = account.AccountNumber,
        Bsb = account.Bsb,
        Amount = depositDetail.Amount,
        Reference = depositDetail.Reference
      };

      _paymentRepository.Add(entity);

      return id;
    }
  }
}
