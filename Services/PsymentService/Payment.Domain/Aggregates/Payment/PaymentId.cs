using BuildingBlocks.Domain.Errors;

namespace Payment.Domain.Aggregates.Payment
{
    public record PaymentId(Guid Value)
    {
        public static PaymentId New()
            => new PaymentId(Guid.CreateVersion7());

        public static Result<PaymentId> From(Guid value)
        {
            if (value == Guid.Empty)
                return Result.Failure<PaymentId>(PaymentErrors.InvalidId);

            return Result.Success<PaymentId>(new PaymentId(value)); ;
        }
    }
}
