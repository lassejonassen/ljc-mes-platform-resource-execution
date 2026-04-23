using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Commands;

public sealed record UpdateWorkCenterCommand(Guid Id, string Name, string? Description) : IRequest<Result>;

public sealed class UpdateWorkCenterCommandHandler(
    IWorkCenterRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateWorkCenterCommand, Result>
{
    public async Task<Result> Handle(UpdateWorkCenterCommand request, CancellationToken cancellationToken)
    {
        var workCenter = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (workCenter is null)
            return Result.Failure(WorkCenterErrors.NotFound);

        var updateResult = workCenter.Update(request.Name, request.Description);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}