using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.EquipmentClasses.Commands.StandardCapabilities;

public sealed record DeleteStandardCapabilityCommand(Guid EquipmentClassId, string Name) : IRequest<Result>;

public sealed class DeleteStandardCapabilityCommandHandler(
    IEquipmentClassRepository equipmentClassRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteStandardCapabilityCommand, Result>
{
    public async Task<Result> Handle(DeleteStandardCapabilityCommand request, CancellationToken cancellationToken)
    {
        var entity = await equipmentClassRepository.GetByIdAsync(request.EquipmentClassId, cancellationToken);
        if (entity is null)
            return Result.Failure(EquipmentClassErrors.NotFound);

        var result = entity.RemoveStandardCapability(request.Name);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
