using BuildingBlocks.Domain.Errors;
using Payment.Domain.Aggregates.Payment;

namespace Payment.Domain
{
    public class PaymentErrors
    {
        public static readonly Error InvalidId = new Error(
         "payment.invalid.id",
         ErrorType.Validation);

        public static Error ProductNotFound(PaymentId paymentId) => new Error(
         $"payment.paymentNotFound.{paymentId.Value.ToString()}",
         ErrorType.NotFound);
    }
}
