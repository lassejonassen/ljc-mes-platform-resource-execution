using ResourceExecution.Domain._Shared.DomainEvents;

namespace ResourceExecution.Domain._Shared;

public interface IHasDomainEvents
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}
