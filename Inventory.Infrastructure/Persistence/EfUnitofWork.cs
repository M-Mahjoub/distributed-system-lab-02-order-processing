using BuildingBlocks.Application.Abstractions.Persistence;
using BuildingBlocks.Domain.Errors;
using Inventory.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Persistence
{
    public class EfUnitofWork : IUnitOfWork
    {
        public InventoryDbContext _dbContext { get; set; }

        public EfUnitofWork(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> CommitAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Result.Success();
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
