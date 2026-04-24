using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Commands.Units;

public sealed record CreateUnitCommand(Guid WorkCenterId, string Name, string? Description, Guid EquipmentClassId) : IRequest<Result>;

public sealed class CreateUnitCommandHandler(
    IWorkCenterRepository repository,
    IEquipmentClassRepository equipmentClassRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateUnitCommand, Result>
{
    public async Task<Result> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var workCenter = await repository.GetByIdAsync(request.WorkCenterId, cancellationToken);

        if (workCenter is null)
            return Result.Failure(WorkCenterErrors.NotFound);

        var equipmentClass = await equipmentClassRepository.GetByIdAsync(request.EquipmentClassId, cancellationToken);

        if (equipmentClass is null)
            return Result.Failure(EquipmentClassErrors.NotFound);

        var workUnit = workCenter.AddWorkUnit(
            request.Name, request.Description, equipmentClass.Id, dateTimeProvider.UtcNow);

        if (workUnit.IsFailure)
            return Result.Failure(workUnit.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}