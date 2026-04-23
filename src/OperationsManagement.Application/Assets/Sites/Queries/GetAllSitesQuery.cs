using OperationsManagement.Application.Assets.Sites.DTOs;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Sites.Queries;

public sealed record GetAllSitesQuery : IRequest<List<SiteDTO>>;

public sealed record GetAllSitesQueryHandler(ISiteRepository repository)
    : IRequestHandler<GetAllSitesQuery, List<SiteDTO>>
{
    public async Task<List<SiteDTO>> Handle(GetAllSitesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(cancellationToken);

        return [.. entities.Select(e => new SiteDTO
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
        })];
    }
}