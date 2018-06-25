using AutoMapper;
using Payment.Api.DTOs;
using Payment.Common.Models;

namespace Payment.Api.App_Start
{
  public class AutoMapperConfig
  {
    public static void Configure()
    {
      Mapper.Initialize((config) =>
      {
        config.CreateMap<PaymentDataDto, DepositDetail>();
        config.CreateMap<PaymentDataDto, BankAccount>();
      });
    }
  }
}