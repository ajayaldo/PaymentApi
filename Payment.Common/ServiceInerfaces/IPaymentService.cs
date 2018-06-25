using Payment.Common.Models;

namespace Payment.Common.ServiceInerfaces
{
  public interface IPaymentService
  {
    string Submit(BankAccount account, DepositDetail depositDetail);
  }
}
