using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Commands;

public sealed record DeleteWorkCenterCommand(Guid Id) : IRequest<Result>;

public sealed class DeleteWorkCenterCommandHandler(
    IWorkCenterRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteWorkCenterCommand, Result>
{
    public async Task<Result> Handle(DeleteWorkCenterCommand request, CancellationToken cancellationToken)
    {
        var workCenter = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (workCenter is null)
            return Result.Failure(WorkCenterErrors.NotFound);

        repository.Delete(workCenter);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}