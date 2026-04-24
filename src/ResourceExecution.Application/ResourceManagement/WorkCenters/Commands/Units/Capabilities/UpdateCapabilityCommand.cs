using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Commands.Units.Capabilities;

public sealed record UpdateCapabilityCommand(Guid WorkCenterId, Guid WorkUnitId, string Name, string Value, string UnitOfMeasure) : IRequest<Result>;

public sealed class UpdateCapabilityCommandHandler(
     IWorkCenterRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCapabilityCommand, Result>
{
    public async Task<Result> Handle(UpdateCapabilityCommand request, CancellationToken cancellationToken)
    {
        var workCenter = await repository.GetByIdAsync(request.WorkCenterId, cancellationToken);
        if (workCenter is null)
            return Result.Failure(EquipmentClassErrors.NotFound);

        var workUnit = workCenter.WorkUnits.FirstOrDefault(x => x.Id == request.WorkUnitId);
        if (workUnit is null)
            return Result.Failure(WorkCenterErrors.WorkUnitNotFound);

        var result = workUnit.UpdateCapability(request.Name, request.Value, request.UnitOfMeasure);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
