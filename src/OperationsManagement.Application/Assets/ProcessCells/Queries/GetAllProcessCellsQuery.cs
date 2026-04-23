using OperationsManagement.Application.Assets.ProcessCells.DTOs;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.ProcessCells.Queries;

public sealed record GetAllProcessCellsQuery(Guid AreaId) : IRequest<IReadOnlyCollection<ProcessCellDTO>>;

public sealed class GetAllProcessCellsQueryHandler(
    IProcessCellRepository repository)
    : IRequestHandler<GetAllProcessCellsQuery, IReadOnlyCollection<ProcessCellDTO>>
{
    public async Task<IReadOnlyCollection<ProcessCellDTO>> Handle(GetAllProcessCellsQuery request, CancellationToken cancellationToken)
    {
        var entites = await repository.GetAllAsync(request.AreaId, cancellationToken);
        return [..entites.Select(x => new ProcessCellDTO
        {
            Id = x.Id,
            AreaId = x.AreaId,
            Name = x.Name,
            Description = x.Description,
            Units = null
        })];
    }
}