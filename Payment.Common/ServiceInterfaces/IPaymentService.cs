using Payment.Common.Models;

namespace Payment.Common.ServiceInterfaces
{
  public interface IPaymentService
  {
    string Submit(BankAccount account, DepositDetail depositDetail);
  }
}
