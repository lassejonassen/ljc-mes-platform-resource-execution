using ResourceExecution.Application.Common.DTOs;
using ResourceExecution.Application.ResourceManagement.EquipmentClasses.DTOs;
using ResourceExecution.Domain.ResourceManagement.Errors;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.EquipmentClasses.Queries;

public sealed record GetEquipmentClassByIdQuery(Guid Id) : IRequest<Result<EquipmentClassDTO>>;

public sealed class GetEquipmentClassByIdQueryHandler(IEquipmentClassRepository repository)
    : IRequestHandler<GetEquipmentClassByIdQuery, Result<EquipmentClassDTO>>
{
    public async Task<Result<EquipmentClassDTO>> Handle(GetEquipmentClassByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return Result.Failure<EquipmentClassDTO>(EquipmentClassErrors.NotFound);
        }
        var dto = new EquipmentClassDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Capabilities = [.. entity.StandardCapabilities.Select(x => new EquipmentCapabilityDTO
            {
                Name = x.Name,
                Value = x.Value,
                UnitOfMeasure = x.UnitOfMeasure
            })]
        };

        return Result.Success(dto);
    }
}