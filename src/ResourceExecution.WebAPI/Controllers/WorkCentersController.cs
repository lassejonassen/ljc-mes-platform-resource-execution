using Microsoft.AspNetCore.Mvc;
using ResourceExecution.Application.ResourceManagement.WorkCenters.Commands;
using ResourceExecution.Application.ResourceManagement.WorkCenters.Commands.Units;
using ResourceExecution.Application.ResourceManagement.WorkCenters.Commands.Units.Capabilities;
using ResourceExecution.Application.ResourceManagement.WorkCenters.Queries;
using ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells;
using ResourceExecution.WebAPI.Contracts.ResourceManagement.ProcessCells.Units;
using ResourceExecution.WebAPI.Contracts.ResourceManagement.WorkCenters.Units;
using ResourceExecution.WebAPI.Contracts.ResourceManagement.WorkCenters.Units.Capabilities;

namespace ResourceExecution.WebAPI.Controllers;

[Route("/api/resources-management/work-centers")]
public class WorkCentersController : BaseController
{
    [ProducesResponseType(typeof(WorkCenterListResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllWorkCentersQuery();

        var result = await Mediator.Send(query, cancellationToken);

        if (!result.Any())
        {
            return NoContent();
        }

        var dtos = result.Select(x => new WorkCenterResponseDTO
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
        });

        var _result = new WorkCenterListResponseDTO
        {
            Data = [.. dtos],
        };

        return Ok(_result);
    }

    [ProducesResponseType(typeof(WorkCenterResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetWorkCenterByIdQuery(id);

        var result = await Mediator.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        var _result = new WorkCenterResponseDTO
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Description = result.Value.Description,
            WorkUnits = result.Value.WorkUnits?.Select(x => new UnitResponseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                EquipmentClassId = x.EquipmentClassId,
                WorkCenterId = x.WorkCenterId

            }).ToList()
        };

        return Ok(_result);
    }

    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkCenterCreateRequestDTO request, CancellationToken cancellationToken)
    {
        var query = new CreateWorkCenterCommand(request.Name, request.Description);

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
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] WorkCenterUpdateRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Id in route does not match Id in body.");
        }

        var query = new UpdateWorkCenterCommand(id, request.Name, request.Description);

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

        var query = new DeleteWorkCenterCommand(id);

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
    [HttpPost("{id:guid}/units")]
    public async Task<IActionResult> CreateUnit([FromRoute] Guid id, [FromBody] UnitCreateRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.EquipmentClassId)
        {
            return BadRequest("ID in route does not match EquipmentClassId in body.");
        }

        var command = new CreateUnitCommand(
            id,
            request.Name,
            request.Description,
            request.EquipmentClassId
            );

        var result = await Mediator.Send(command, cancellationToken);

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
    [HttpPatch("{id:guid}/units{workUnitId:guid}")]
    public async Task<IActionResult> UpdateUnit([FromRoute] Guid id, [FromRoute] Guid workUnitId, [FromBody] UnitUpdateRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.WorkCenterId)
        {
            return BadRequest("ID in route does not match  in body.");
        }

        if (workUnitId != request.WorkUnitId)
        {
            return BadRequest("ID in route does not match  in body.");
        }

        var command = new UpdateUnitCommand(
            id,
            request.WorkUnitId,
            request.Name,
            request.Description,
            request.EquipmentClassId);

        var result = await Mediator.Send(command, cancellationToken);

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
    [HttpDelete("{id:guid}/units/{workUnitId:guid}")]
    public async Task<IActionResult> DeleteUnit([FromRoute] Guid id, [FromRoute] Guid workUnitId, CancellationToken cancellationToken)
    {
        var command = new DeleteUnitCommand(
            id,
            workUnitId);

        var result = await Mediator.Send(command, cancellationToken);

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
    [HttpPost("{id:guid}/units/{unitId:guid}/capabilities")]
    public async Task<IActionResult> CreateCapability([FromRoute] Guid id, [FromRoute] Guid unitId, [FromBody] UnitCapabilityCreateRequestDTO request, CancellationToken cancellationToken)
    {
        var command = new CreateCapabilityCommand(
            id,
            unitId,
            request.Name,
            request.Value,
            request.UnitOfMeasure
            );

        var result = await Mediator.Send(command, cancellationToken);

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
    [HttpPatch("{id:guid}/units/{unitId:guid}/capabilities")]
    public async Task<IActionResult> UpdateCapability([FromRoute] Guid id, [FromRoute] Guid unitId, [FromBody] UnitCapabilityUpdateRequestDTO request, CancellationToken cancellationToken)
    {
        var command = new UpdateCapabilityCommand(
            id,
            unitId,
            request.Name,
            request.Value,
            request.UnitOfMeasure);

        var result = await Mediator.Send(command, cancellationToken);

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
    [HttpDelete("{id:guid}/units/{unitId:guid}/capabilities")]
    public async Task<IActionResult> DeleteCapability([FromRoute] Guid id, [FromRoute] Guid unitId, [FromRoute] UnitCapabilityDeleteRequestDTO request, CancellationToken cancellationToken)
    {
        var command = new DeleteCapabilityCommand(id, unitId, request.Name);

        var result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        return NoContent();
    }
}
