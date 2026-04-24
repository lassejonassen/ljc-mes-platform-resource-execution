using ResourceExecution.SharedKernel;

namespace ResourceExecution.Infrastructure;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}