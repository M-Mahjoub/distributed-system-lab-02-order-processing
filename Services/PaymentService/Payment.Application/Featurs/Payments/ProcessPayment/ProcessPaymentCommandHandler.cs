//using BuildingBlocks.Application;
//using BuildingBlocks.Domain.Errors;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Payment.Application.Featurs.Payments.ProcessPayment
//{
//    public sealed class ProcessPaymentCommandHandler
//     : IRequestHandler<ProcessPaymentCommand, Result>
//    {
//        private readonly IPaymentRepository _repository;
//        private readonly IUnitOfWork _unitOfWork;

//        public ProcessPaymentCommandHandler(
//            IPaymentRepository repository,
//            IUnitOfWork unitOfWork)
//        {
//            _repository = repository;
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<Result> Handle(
//            ProcessPaymentCommand command,
//            CancellationToken cancellationToken)
//        {
//            var payment =
//                await _repository.GetByOrderIdAsync(
//                    command.OrderId,
//                    cancellationToken);

//            if (payment is null)
//            {
//                return Result.Failure(
//                    PaymentErrors.NotFound);
//            }

//            var result = payment.Process();

//            if (result.IsFailure)
//                return result;

//            await _unitOfWork.CommitAsync(
//                cancellationToken);

//            return Result.Success();
//        }
//    }
//}
