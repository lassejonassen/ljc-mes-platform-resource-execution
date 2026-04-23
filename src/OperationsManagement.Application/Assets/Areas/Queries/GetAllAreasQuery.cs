using OperationsManagement.Application.Assets.Areas.DTOs;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Areas.Queries;

public sealed record GetAllAreasQuery(Guid SiteId) : IRequest<List<AreaDTO>>;

public sealed class GetAllAreasQueryHandler(IAreaRepository repository)
    : IRequestHandler<GetAllAreasQuery, List<AreaDTO>>
{
    public async Task<List<AreaDTO>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.SiteId, cancellationToken);

        return [.. entities.Select(e => new AreaDTO
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            SiteId = e.SiteId
        })];
    }
}