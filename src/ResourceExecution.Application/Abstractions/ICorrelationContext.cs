namespace ResourceExecution.Application.Abstractions;

public interface ICorrelationContext
{
    Guid CorrelationId { get; }
}