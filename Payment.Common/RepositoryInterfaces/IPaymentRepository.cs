using Payment.Common.Entities;

namespace Payment.Common.RepositoryInterfaces
{
  public interface IPaymentRepository
  {
    void Add(PaymentDataEntity paymentEntity);
  }
}
