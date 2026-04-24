using ResourceExecution.SharedKernel.IntegrationEvents;

namespace ResourceExecution.Application.Abstractions.IntegrationEvents;

public interface IIntegrationEventPublisher
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken ct = default)
        where T : class, IIntegrationEvent;
}
