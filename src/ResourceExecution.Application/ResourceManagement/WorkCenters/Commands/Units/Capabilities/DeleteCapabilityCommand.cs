using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Commands.Units.Capabilities;

public sealed record DeleteCapabilityCommand(Guid WorkCenterId, Guid WorkUnitId, string Name) : IRequest<Result>;

public sealed class DeleteCapabilityCommandHandler(
    IWorkCenterRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCapabilityCommand, Result>
{
    public async Task<Result> Handle(DeleteCapabilityCommand request, CancellationToken cancellationToken)
    {
        var workCenter = await repository.GetByIdAsync(request.WorkCenterId, cancellationToken);
        if (workCenter is null)
            return Result.Failure(EquipmentClassErrors.NotFound);

        var workUnit = workCenter.WorkUnits.FirstOrDefault(x => x.Id == request.WorkUnitId);
        if (workUnit is null)
            return Result.Failure(WorkCenterErrors.WorkUnitNotFound);

        var result = workUnit.RemoveCapability(request.Name);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
