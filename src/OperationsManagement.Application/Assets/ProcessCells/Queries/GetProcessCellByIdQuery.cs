using OperationsManagement.Application.Assets.ProcessCells.DTOs;
using OperationsManagement.Domain.Assets.Errors;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.ProcessCells.Queries;

public sealed record GetProcessCellByIdQuery(Guid Id) : IRequest<Result<ProcessCellDTO>>;

public sealed class GetProcessCellByIdQueryHandler(
    IProcessCellRepository repository)
    : IRequestHandler<GetProcessCellByIdQuery, Result<ProcessCellDTO>>
{
    public async Task<Result<ProcessCellDTO>> Handle(GetProcessCellByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
            return Result.Failure<ProcessCellDTO>(ProcessCellErrors.NotFound);

        var dto = new ProcessCellDTO
        {
            Id = entity.Id,
            AreaId = entity.AreaId,
            Name = entity.Name,
            Description = entity.Description,
            Units = [.. entity.Units.Select(x => new UnitDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UnsEnterprise = x.UnsAddress?.Enterprise,
                UnsSite = x.UnsAddress?.Site,
                UnsArea = x.UnsAddress?.Area,
                UnsProcessCell = x.UnsAddress?.ProcessCell,
                UnsUnit = x.UnsAddress?.Unit,
                ProcessCellId = x.ProcessCellId,
                ProcessSegmentId = x.ProcessSegmentId
            })]
        };

        return Result.Success(dto);
    }
}
