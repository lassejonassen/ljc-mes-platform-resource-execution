using OperationsManagement.Domain.Assets.Errors;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Areas.Commands;

public sealed record DeleteAreaCommand(Guid Id) : IRequest<Result>;

public sealed class DeleteAreaCommandHandler(
    IAreaRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAreaCommand, Result>
{
    public async Task<Result> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
    {
        var area = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (area is null)
            return Result.Failure(AreaErrors.NotFound);

        repository.Delete(area);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}