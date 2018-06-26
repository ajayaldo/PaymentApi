using System.IO;
using System.Threading;
using Payment.Common.Entities;
using Payment.Common.RepositoryInterfaces;
using Payment.Common.ServiceInterfaces;

namespace Payment.Repository
{
  public class TextPaymentRepository
    : IPaymentRepository
  {
    private readonly IFilePathService _filePathService;
    static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

    public TextPaymentRepository(IFilePathService filePathService)
    {
      _filePathService = filePathService;
    }

    public void Add(PaymentDataEntity paymentEntity)
    {
      Locker.EnterWriteLock();
      try
      {
        var mappedPath = _filePathService.GetPaymentDataFilePath();

        using (var sw = File.AppendText(mappedPath))
        {
          sw.WriteLine($"{paymentEntity.Id},{paymentEntity.AccountName},{paymentEntity.AccountNumber},{paymentEntity.Amount},{ paymentEntity.Bsb},{ paymentEntity.Reference}");
        }
      }
      finally
      {
        Locker.ExitWriteLock();
      }
    }
  }
}
