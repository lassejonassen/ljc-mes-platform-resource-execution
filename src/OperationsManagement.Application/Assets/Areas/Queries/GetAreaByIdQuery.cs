using OperationsManagement.Application.Assets.Areas.DTOs;
using OperationsManagement.Domain.Assets.Errors;
using OperationsManagement.Domain.Assets.Repositories;
using OperationsManagement.SharedKernel;
using OperationsManagement.SharedKernel.Messaging;

namespace OperationsManagement.Application.Assets.Areas.Queries;

public sealed record GetAreaByIdQuery(Guid Id) : IRequest<Result<AreaDTO>>;

public sealed class GetAreaByIdQueryHandler(IAreaRepository repository)
    : IRequestHandler<GetAreaByIdQuery, Result<AreaDTO>>
{
    public async Task<Result<AreaDTO>> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return Result.Failure<AreaDTO>(AreaErrors.NotFound);
        }
        var dto = new AreaDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            SiteId = entity.SiteId,
        };

        return Result.Success(dto);
    }
}