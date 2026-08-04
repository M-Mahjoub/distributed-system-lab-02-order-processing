namespace BuildingBlocks.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        public DateTime CreateAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public long Version { get; protected set; }
        protected AggregateRoot()
        {

        }

        protected AggregateRoot(TId id) : base(id)
        {
            CreateAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected Result CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
                return Result.Failure(rule.Error);

            return Result.Success();
        }

        protected static Result Ensure(params IBusinessRule[] rules)
        {
            foreach (var rule in rules)
            {
                if (rule.IsBroken())
                    return Result.Failure(rule.Error);
            }

            return Result.Success();
        }
    }
}
