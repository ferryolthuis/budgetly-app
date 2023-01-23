using MediatR;
using Shared.Domain.Primitives;

namespace Shared.Application.Messaging;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
