using BuildingBlocks.Domain.Common;
using BuildingBlocks.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.Persistence.Extensions
{
    public static class DbContextExtensions
    {
        public static IReadOnlyCollection<IAggregateRoot>
        GetAggregatesWithEvents(
            this DbContext context)
        {
            //1.Aggregateهای تغییر کرده را پیدا کن
            return context
                .ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();
        }

        public static IReadOnlyCollection<IDomainEvent>
    GetDomainEvents(
        this DbContext context)
        {
            //2.DomainEventها را جمع کن
            return context
                .GetAggregatesWithEvents()
                .SelectMany(x => x.DomainEvents)
                .ToList();
        }

        public static void ClearDomainEvents(
                           this DbContext context)
        {
            foreach (var aggregate in context.GetAggregatesWithEvents())
            {
                aggregate.ClearDomainEvents();
            }
        }
    }
}
