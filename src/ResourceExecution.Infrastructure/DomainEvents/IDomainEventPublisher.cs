using ResourceExecution.Domain._Shared.DomainEvents;

namespace ResourceExecution.Infrastructure.DomainEvents;

public interface IDomainEventPublisher
{
    Task PublishAsync(IDomainEvent domainEvent, CancellationToken ct = default);
}