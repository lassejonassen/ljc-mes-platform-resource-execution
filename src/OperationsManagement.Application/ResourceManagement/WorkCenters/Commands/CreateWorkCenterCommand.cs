using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Commands;

public sealed record CreateWorkCenterCommand(string Name, string? Description) : IRequest<Result<Guid>>;

public sealed class CreateWorkCenterCommandHandler(
    IWorkCenterRepository repository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateWorkCenterCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateWorkCenterCommand request, CancellationToken cancellationToken)
    {
        var isNameUnique = await repository.IsNameUniqueAsync(request.Name, cancellationToken);

        if (!isNameUnique)
            return Result.Failure<Guid>(WorkCenterErrors.AlreadyExists);

        var workCenter = WorkCenter.Create(request.Name, request.Description, dateTimeProvider.UtcNow);

        if (workCenter.IsFailure)
            return Result.Failure<Guid>(workCenter.Error);

        repository.Add(workCenter.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(workCenter.Value.Id);
    }
}