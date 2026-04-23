using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.EquipmentClasses.Commands.StandardCapabilities;

public sealed record CreateStandardCapabilityCommand(Guid EquipmentClassId, string Name, string Value, string UnitOfMeasure) : IRequest<Result>;

public sealed class CreateStandardCapabilityCommandHandler(
    IEquipmentClassRepository equipmentClassRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateStandardCapabilityCommand, Result>
{
    public async Task<Result> Handle(CreateStandardCapabilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await equipmentClassRepository.GetByIdAsync(request.EquipmentClassId, cancellationToken);
        if (entity is null)
            return Result.Failure(EquipmentClassErrors.NotFound);

        var result = entity.AddStandardCapability(request.Name, request.Value, request.UnitOfMeasure);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
