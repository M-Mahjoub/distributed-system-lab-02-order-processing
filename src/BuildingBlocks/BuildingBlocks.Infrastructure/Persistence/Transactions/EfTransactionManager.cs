using Inventory.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.Persistence.Transactions;

public sealed class EfTransactionManager<TDbContext>
    : ITransactionManager
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public EfTransactionManager(
        TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task ExecuteAsync(
        Func<CancellationToken, Task> action,
        CancellationToken cancellationToken = default)
    {
        await using var transaction =
            await _dbContext.Database.BeginTransactionAsync(
                cancellationToken);

        try
        {
            await action(cancellationToken);

            await transaction.CommitAsync(
                cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(
                cancellationToken);

            throw;
        }
    }
}