using OperationsManagement.Domain.Assets.Aggregates;
using OperationsManagement.Domain.Assets.Errors;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Areas.Commands;

public sealed record CreateAreaCommand(Guid SiteId, string Name, string? Description) : IRequest<Result<Guid>>;

public sealed class CreateAreaCommandHandler(
    IAreaRepository repository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateAreaCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
    {
        var isNameUnique = await repository.IsNameUniqueAsync(request.SiteId, request.Name, cancellationToken);

        if (!isNameUnique)
            return Result.Failure<Guid>(AreaErrors.AlreadyExists);

        var area = Area.Create(request.Name, request.Description, request.SiteId, dateTimeProvider.UtcNow);

        if (area.IsFailure)
            return Result.Failure<Guid>(area.Error);

        repository.Add(area.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(area.Value.Id);
    }
}