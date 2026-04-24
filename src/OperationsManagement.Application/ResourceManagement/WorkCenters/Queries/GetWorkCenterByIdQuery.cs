using ResourceExecution.Application.Common.DTOs;
using ResourceExecution.Application.ResourceManagement.WorkCenters.DTOs;
using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Queries;

public sealed record GetWorkCenterByIdQuery(Guid Id) : IRequest<Result<WorkCenterDTO>>;

public sealed class GetWorkCenterByIdQueryHandler(
    IWorkCenterRepository repository)
    : IRequestHandler<GetWorkCenterByIdQuery, Result<WorkCenterDTO>>
{
    public async Task<Result<WorkCenterDTO>> Handle(GetWorkCenterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
            return Result.Failure<WorkCenterDTO>(WorkCenterErrors.NotFound);

        var dto = new WorkCenterDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            WorkUnits = [.. entity.WorkUnits.Select(x => new WorkUnitDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                WorkCenterId = x.Id,
                EquipmentClassId = x.EquipmentClassId,
                Status = x.Status.ToString(),
                Capabilities = [.. x.Capabilities.Select(x => new EquipmentCapabilityDTO
                {
                    Name = x.Name,
                    Value = x.Value,
                    UnitOfMeasure = x.UnitOfMeasure
                })]
            })]
        };

        return Result.Success(dto);
    }
}
