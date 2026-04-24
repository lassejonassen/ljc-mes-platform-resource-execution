namespace ResourceExecution.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}