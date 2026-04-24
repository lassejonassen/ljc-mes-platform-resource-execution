using ResourceExecution.SharedKernel.IntegrationEvents;

namespace ResourceExecution.Infrastructure.Messaging;

public interface IIntegrationEventBus
{
    Task SendAsync<T>(T integrationEvent, CancellationToken ct = default)
        where T : class, IIntegrationEvent;
}
