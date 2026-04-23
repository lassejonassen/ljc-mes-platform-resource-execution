using OperationsManagement.Application.Assets.Sites.DTOs;
using OperationsManagement.Domain.Assets.Errors;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Sites.Queries;

public sealed record GetSiteByIdQuery(Guid Id) : IRequest<Result<SiteDTO>>;

public sealed class GetSiteByIdQueryHandler(ISiteRepository repository)
    : IRequestHandler<GetSiteByIdQuery, Result<SiteDTO>>
{
    public async Task<Result<SiteDTO>> Handle(GetSiteByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return Result.Failure<SiteDTO>(SiteErrors.NotFound);
        }
        var dto = new SiteDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
        return Result.Success(dto);
    }
}