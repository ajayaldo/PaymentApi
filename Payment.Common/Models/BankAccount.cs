namespace Payment.Common.Models
{
  public sealed class BankAccount
  {
    public int Bsb { get; set; }
    public long AccountNumber { get; set; }
    public string AccountName { get; set; }
  }
}
