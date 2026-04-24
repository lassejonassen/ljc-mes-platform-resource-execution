using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.EquipmentClasses.Commands;

public sealed record DeleteEquipmentClassCommand(Guid Id) : IRequest<Result>;

public sealed class DeleteEquipmentClassCommandHandler(
    IEquipmentClassRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEquipmentClassCommand, Result>
{
    public async Task<Result> Handle(DeleteEquipmentClassCommand request, CancellationToken cancellationToken)
    {
        var equipmentclass = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (equipmentclass is null)
            return Result.Failure(EquipmentClassErrors.NotFound);

        repository.Delete(equipmentclass);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}