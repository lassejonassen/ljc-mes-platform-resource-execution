using Microsoft.AspNetCore.Mvc;
using OperationsManagement.Application.Assets.Areas.Commands;
using OperationsManagement.Application.Assets.ProcessCells.Queries;
using OperationsManagement.WebAPI.Contracts.Assets.Areas;
using OperationsManagement.WebAPI.Contracts.Assets.ProcessCells;
using OperationsManagement.WebAPI.Contracts.Assets.ProcessCells.Units;

namespace OperationsManagement.WebAPI.Controllers;

[Route("/api/assets/process-cells")]
public class ProcessCellsController : BaseController
{
    [ProducesResponseType(typeof(ProcessCellListResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid areaId, CancellationToken cancellationToken)
    {
        var query = new GetAllProcessCellsQuery(areaId);

        var result = await Mediator.Send(query, cancellationToken);

        if (!result.Any())
        {
            return NoContent();
        }

        var dtos = result.Select(x => new ProcessCellResponseDTO
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            AreaId = x.AreaId
        });

        var _result = new ProcessCellListResponseDTO
        {
            Data = dtos,
        };

        return Ok(_result);
    }

    [ProducesResponseType(typeof(ProcessCellResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProcessCellByIdQuery(id);

        var result = await Mediator.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        var _result = new ProcessCellResponseDTO
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Description = result.Value.Description,
            AreaId = result.Value.AreaId,
            Units = result.Value.Units?.Select(x => new UnitResponseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                UnsEnterprise = x.UnsEnterprise,
                UnsSite = x.UnsSite,
                UnsArea = x.UnsArea,
                UnsProcessCell = x.UnsProcessCell,
                UnsUnit = x.UnsUnit,
                ProcessCellId = x.ProcessCellId,
                ProcessSegmentId = x.ProcessSegmentId
            }).ToList()
        };

        return Ok(_result);
    }

    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AreaCreateRequestDTO request, CancellationToken cancellationToken)
    {
        var query = new CreateAreaCommand(request.SiteId, request.Name, request.Description);

        var result = await Mediator.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        return CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AreaUpdateRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Id in route does not match Id in body.");
        }

        var query = new UpdateAreaCommand(id, request.Name, request.Description);

        var result = await Mediator.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        return NoContent();
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("The id cannot be empty");
        }

        var query = new DeleteAreaCommand(id);

        var result = await Mediator.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        return NoContent();
    }
}
