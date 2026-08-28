//using BuildingBlocks.Domain.Common;
//using BuildingBlocks.Domain.Errors;
//using Payment.Domain.Aggregates.Payment.Events;

//namespace Payment.Domain.Aggregates.Payment
//{
//    public class Payment : AggregateRoot<PaymentId>
//    {
//        private Payment()
//        {
                
//        }

//        public static Payment Create()
//        {
//            return new Payment();
//        }

//        public Result Process()
//        {
//            // فعلاً فرض می‌کنیم پرداخت موفق است.

//            Status = PaymentStatus.Succeeded;

//            Raise(
//                new PaymentSucceededDomainEvent(
//                    Guid.NewGuid(),
//                    DateTime.UtcNow,
//                    OrderId));

//            return Result.Success();
//        }
//        public Result MarkFailed()
//        {
//            if (Status == PaymentStatus.Succeeded)
//            {
//                return Result.Failure(
//                    PaymentErrors.AlreadySucceeded);
//            }

//            Status = PaymentStatus.Failed;

//            Raise(
//                new PaymentFailedDomainEvent(
//                    Guid.NewGuid(),
//                    DateTime.UtcNow,
//                    OrderId));

//            return Result.Success();
//        }

//    }
//}
