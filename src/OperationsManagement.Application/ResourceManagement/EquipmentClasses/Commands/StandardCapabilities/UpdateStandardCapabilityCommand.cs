using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.EquipmentClasses.Commands.StandardCapabilities;

public sealed record UpdateStandardCapabilityCommand(Guid EquipmentClassId, string Name, string Value, string UnitOfMeasure) : IRequest<Result>;

public sealed class UpdateStandardCapabilityCommandHandler(
    IEquipmentClassRepository equipmentClassRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateStandardCapabilityCommand, Result>
{
    public async Task<Result> Handle(UpdateStandardCapabilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await equipmentClassRepository.GetByIdAsync(request.EquipmentClassId, cancellationToken);
        if (entity is null)
            return Result.Failure(EquipmentClassErrors.NotFound);

        var result = entity.UpdateStandardCapability(request.Name, request.Value, request.UnitOfMeasure);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
