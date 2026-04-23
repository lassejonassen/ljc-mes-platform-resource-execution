using OperationsManagement.Domain.Assets.Errors;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Sites.Commands;

public sealed record DeleteSiteCommand(Guid Id) : IRequest<Result>;

public sealed class DeleteSiteCommandHandler(
    ISiteRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteSiteCommand, Result>
{
    public async Task<Result> Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
    {
        var site = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (site is null)
            return Result.Failure(SiteErrors.NotFound);

        repository.Delete(site);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}