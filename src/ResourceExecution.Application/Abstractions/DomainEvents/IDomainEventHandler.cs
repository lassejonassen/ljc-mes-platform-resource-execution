using ResourceExecution.Domain._Shared.DomainEvents;

namespace ResourceExecution.Application.Abstractions.DomainEvents;

public interface IDomainEventHandler<in TEvent>
    where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken ct = default);
}
