using Inventory.Application.Abstractions.Persistence;
using MediatR;

namespace BuildingBlocks.Application.Behaviors;

//اگر Commandهای ما با MediatR اجرا می‌شوند، بهترین جا برای Command transaction همین Pipeline است.
//قبل از رسیدن به Handler از این مسیر عبور می‌کند:
public sealed class TransactionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ITransactionManager _transactionManager;

    public TransactionBehavior(
        ITransactionManager transactionManager)
    {
        _transactionManager = transactionManager;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? response = default;

        await _transactionManager.ExecuteAsync(
            async ct =>
            {
                response = await next();
            },
            cancellationToken);

        return response!;
    }
}