using ResourceExecution.Application.ResourceManagement.EquipmentClasses.DTOs;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.EquipmentClasses.Queries;

public sealed record GetAllEquipmentClassesQuery : IRequest<IReadOnlyCollection<EquipmentClassDTO>>;

public sealed class GetAllEquipmentClassesQueryHandler(IEquipmentClassRepository repository)
    : IRequestHandler<GetAllEquipmentClassesQuery, IReadOnlyCollection<EquipmentClassDTO>>
{
    public async Task<IReadOnlyCollection<EquipmentClassDTO>> Handle(GetAllEquipmentClassesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(cancellationToken);

        return [.. entities.Select(e => new EquipmentClassDTO
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
        })];
    }
}