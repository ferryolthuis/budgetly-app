using Shared.Domain.Primitives;

namespace Shared.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
