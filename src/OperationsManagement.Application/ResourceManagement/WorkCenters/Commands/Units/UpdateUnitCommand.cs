using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Commands.Units;

public sealed record UpdateUnitCommand(Guid WorkCenterId, Guid WorkUnitId, string Name, string? Description, Guid EquipmentClassId) : IRequest<Result>;

public sealed class UpdateUnitCommandHandler(
    IWorkCenterRepository repository,
    IEquipmentClassRepository equipmentClassRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUnitCommand, Result>
{
    public async Task<Result> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var workCenter = await repository.GetByIdAsync(request.WorkCenterId, cancellationToken);

        if (workCenter is null)
            return Result.Failure(WorkCenterErrors.NotFound);

        var equipmentClass = await equipmentClassRepository.GetByIdAsync(request.EquipmentClassId, cancellationToken);

        if (equipmentClass is null)
            return Result.Failure(EquipmentClassErrors.NotFound);

        var result = workCenter.UpdateUnit(request.WorkUnitId, request.Name, request.Description, request.EquipmentClassId);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}