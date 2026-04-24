using ResourceExecution.Domain.ResourceManagement.Aggregates;
using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.EquipmentClasses.Commands;

public sealed record CreateEquipmentClassCommand(string Name, string? Description) : IRequest<Result<Guid>>;

public sealed class CreateAreaCommandHandler(
    IEquipmentClassRepository repository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateEquipmentClassCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateEquipmentClassCommand request, CancellationToken cancellationToken)
    {
        var isNameUnique = await repository.IsNameUniqueAsync(request.Name, cancellationToken);

        if (!isNameUnique)
            return Result.Failure<Guid>(EquipmentClassErrors.AlreadyExists);

        var equipmentClass = EquipmentClass.Create(request.Name, request.Description, dateTimeProvider.UtcNow);

        if (equipmentClass.IsFailure)
            return Result.Failure<Guid>(equipmentClass.Error);

        repository.Add(equipmentClass.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(equipmentClass.Value.Id);
    }
}