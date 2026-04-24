using ResourceExecution.Application.ResourceManagement.WorkCenters.DTOs;
using ResourceExecution.Domain.ResourceManagement.Repositories;
using ResourceExecution.SharedKernel.Messaging;

namespace ResourceExecution.Application.ResourceManagement.WorkCenters.Queries;

public sealed record GetAllWorkCentersQuery : IRequest<IReadOnlyCollection<WorkCenterDTO>>;

public sealed class GetAllWorkCentersQueryHandler(
    IWorkCenterRepository repository)
    : IRequestHandler<GetAllWorkCentersQuery, IReadOnlyCollection<WorkCenterDTO>>
{
    public async Task<IReadOnlyCollection<WorkCenterDTO>> Handle(GetAllWorkCentersQuery request, CancellationToken cancellationToken)
    {
        var entites = await repository.GetAllAsync(cancellationToken);
        return [..entites.Select(x => new WorkCenterDTO
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            WorkUnits = null
        })];
    }
}