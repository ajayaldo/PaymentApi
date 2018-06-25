namespace Payment.Common.Entities
{
  public sealed class PaymentDataEntity
  {
    public int Bsb { get; set; }
    public long AccountNumber { get; set; }
    public string AccountName { get; set; }
    public double Amount { get; set; }
    public string Reference { get; set; }
  }
}
