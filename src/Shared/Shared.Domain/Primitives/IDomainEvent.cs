using MediatR;

namespace Shared.Domain.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
