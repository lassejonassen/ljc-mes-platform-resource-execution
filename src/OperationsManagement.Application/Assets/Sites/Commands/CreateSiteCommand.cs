using OperationsManagement.Domain.Assets.Aggregates;
using OperationsManagement.Domain.Assets.Errors;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Sites.Commands;

public sealed record CreateSiteCommand(string Name, string? Description) : IRequest<Result<Guid>>;

public sealed class CreateSiteCommandHandler(
    ISiteRepository repository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateSiteCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        var isSkuUnique = await repository.IsNameUniqueAsync(request.Name, cancellationToken);

        if (!isSkuUnique)
            return Result.Failure<Guid>(SiteErrors.AlreadyExists);

        var site = Site.Create(request.Name, request.Description, dateTimeProvider.UtcNow);

        if (site.IsFailure)
            return Result.Failure<Guid>(site.Error);

        repository.Add(site.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(site.Value.Id);
    }
}