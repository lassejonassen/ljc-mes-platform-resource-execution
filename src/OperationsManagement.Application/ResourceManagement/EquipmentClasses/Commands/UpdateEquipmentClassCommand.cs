using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.EquipmentClasses.Commands;

public sealed record UpdateEquipmentClassCommand(Guid Id, string Name, string? Description) : IRequest<Result>;

public sealed class UpdateAreaCommandHandler(
    IEquipmentClassRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateEquipmentClassCommand, Result>
{
    public async Task<Result> Handle(UpdateEquipmentClassCommand request, CancellationToken cancellationToken)
    {
        var equipment = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (equipment is null)
            return Result.Failure(EquipmentClassErrors.NotFound);

        var updateResult = equipment.Update(request.Name, request.Description);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}