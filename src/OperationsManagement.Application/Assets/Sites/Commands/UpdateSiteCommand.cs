using OperationsManagement.Domain.Assets.Errors;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Sites.Commands;

public sealed record UpdateSiteCommand(Guid Id, string Name, string? Description) : IRequest<Result>;

public sealed class UpdateSiteCommandHandler(
    ISiteRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateSiteCommand, Result>
{
    public async Task<Result> Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
    {
        var site = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (site is null)
            return Result.Failure(SiteErrors.NotFound);

        var updateResult = site.Update(request.Name, request.Description);

        if (updateResult.IsFailure)
            return Result.Failure(updateResult.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}