using System.ComponentModel.DataAnnotations;

namespace Payment.Api.DTOs
{
  public sealed class PaymentDataDto
  {
    [Required]
    public int Bsb { get; set; }

    [Required]
    public long AccountNumber { get; set; }

    [Required]
    public string AccountName { get; set; }

    [Range(1, double.MaxValue)]
    public double Amount { get; set; }

    [Required]
    public string Reference { get; set; }
  }
}