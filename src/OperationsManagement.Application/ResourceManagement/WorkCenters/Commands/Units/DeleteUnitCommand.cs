using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Commands.Units;

public sealed record DeleteUnitCommand(Guid WorkCenterId, Guid WorkUnitId) : IRequest<Result>;

public sealed class DeleteUnitCommandHandler(
    IWorkCenterRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUnitCommand, Result>
{
    public async Task<Result> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        var workCenter = await repository.GetByIdAsync(request.WorkCenterId, cancellationToken);

        if (workCenter is null)
            return Result.Failure(WorkCenterErrors.NotFound);

        var result = workCenter.RemoveUnit(request.WorkUnitId);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}