using Microsoft.AspNetCore.Mvc;
using ResourceExecution.Application.ResourceManagement.EquipmentClasses.Commands;
using ResourceExecution.Application.ResourceManagement.EquipmentClasses.Commands.StandardCapabilities;
using ResourceExecution.Application.ResourceManagement.EquipmentClasses.Queries;
using ResourceExecution.WebAPI.Contracts;
using ResourceExecution.WebAPI.Contracts.ResourceManagement.EquipmentClasses;
using ResourceExecution.WebAPI.Contracts.ResourceManagement.EquipmentClasses.Capabilties;

namespace ResourceExecution.WebAPI.Controllers;

[Route("/api/resources-management/work-centers")]
public class WorkCentersController : BaseController
{
    [ProducesResponseType(typeof(EquipmentClassListResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllEquipmentClassesQuery();

        var result = await Mediator.Send(query, cancellationToken);

        if (!result.Any())
        {
            return NoContent();
        }

        var dtos = result.Select(x => new EquipmentClassResponseDTO
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
        });

        var _result = new EquipmentClassListResponseDTO
        {
            Data = [.. dtos],
        };

        return Ok(_result);
    }

    [ProducesResponseType(typeof(EquipmentClassResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetEquipmentClassByIdQuery(id);

        var result = await Mediator.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        var _result = new EquipmentClassResponseDTO
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Description = result.Value.Description,
            Capabilities =  result.Value.Capabilities?.Select(x => new EquipmentCapabilityResponseDTO
            {
                Name = x.Name,
                Value = x.Value,
                UnitOfMeasure = x.UnitOfMeasure
            }).ToList()
        };

        return Ok(_result);
    }

    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EquipmentClassCreateRequestDTO request, CancellationToken cancellationToken)
    {
        var query = new CreateEquipmentClassCommand(request.Name, request.Description);

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
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EquipmentClassUpdateRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Id in route does not match Id in body.");
        }

        var query = new UpdateEquipmentClassCommand(id, request.Name, request.Description);

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

        var query = new DeleteEquipmentClassCommand(id);

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
    public async Task<IActionResult> CreateUnit([FromRoute] Guid id, [FromBody] EquipmentClassCapabilityCreateRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.EquipmentClassId)
        {
            return BadRequest("ID in route does not match EquipmentClassId in body.");
        }

        var command = new CreateStandardCapabilityCommand(
            id,
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
    [HttpPatch("{id:guid}/units")]
    public async Task<IActionResult> UpdateUnit([FromRoute] Guid id,[FromBody] EquipmentClassCapabilityUpdateRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.EquipmentClassId)
        {
            return BadRequest("ID in route does not match EquipmentClassId in body.");
        }

        var command = new UpdateStandardCapabilityCommand(
            id,
            request.Name,
            request.Value,
            request.UnitOfMeasure );

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

    [HttpDelete("{id:guid}/units")]
    public async Task<IActionResult> DeleteUnit([FromRoute] Guid id, [FromRoute] EquipmentClassCapabilityDeleteRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.EquipmentClassId)
        {
            return BadRequest("ID in route does not match EquipmentClassId in body.");
        }

        var command = new DeleteStandardCapabilityCommand(
            id,
            request.Name);

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
    public async Task<IActionResult> CreateCapability([FromRoute] Guid id, [FromBody] EquipmentClassCapabilityCreateRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.EquipmentClassId)
        {
            return BadRequest("ID in route does not match EquipmentClassId in body.");
        }

        var command = new CreateStandardCapabilityCommand(
            id,
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
    public async Task<IActionResult> UpdateCapability([FromRoute] Guid id, [FromBody] EquipmentClassCapabilityUpdateRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.EquipmentClassId)
        {
            return BadRequest("ID in route does not match EquipmentClassId in body.");
        }

        var command = new UpdateStandardCapabilityCommand(
            id,
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

    [HttpDelete("{id: guid}/units/{unitId:guid}/ capabilities")]
    public async Task<IActionResult> DeleteCapability([FromRoute] Guid id, [FromRoute] EquipmentClassCapabilityDeleteRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.EquipmentClassId)
        {
            return BadRequest("ID in route does not match EquipmentClassId in body.");
        }

        var command = new DeleteStandardCapabilityCommand(
            id,
            request.Name);

        var result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        return NoContent();
    }
}
