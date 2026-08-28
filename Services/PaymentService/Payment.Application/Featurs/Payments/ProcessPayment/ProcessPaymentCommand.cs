using BuildingBlocks.Domain.Errors;
using MediatR;

namespace Payment.Application.Featurs.Payments.ProcessPayment
{
    public  record ProcessPaymentCommand(
      Guid OrderId)
      : IRequest<Result>;
}
