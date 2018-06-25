using Payment.Common.Entities;

namespace Payment.Common.RepositoryInterfaces
{
  public interface IPaymentRepository
  {
    string SubmitPayment(PaymentDataEntity paymentEntity);
  }
}
