using OperationsManagement.Domain.Assets.Errors;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Areas.Commands;

public sealed record UpdateAreaCommand(Guid Id, string Name, string? Description) : IRequest<Result>;

public sealed class UpdateAreaCommandHandler(
    IAreaRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAreaCommand, Result>
{
    public async Task<Result> Handle(UpdateAreaCommand request, CancellationToken cancellationToken)
    {
        var area = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (area is null)
            return Result.Failure(AreaErrors.NotFound);

        var updateResult = area.Update(request.Name, request.Description);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}