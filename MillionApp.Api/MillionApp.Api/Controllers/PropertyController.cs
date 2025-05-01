using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MillionApp.Application.Commands;
using MillionApp.Application.Queries;
using MillionApp.Domain.Dtos;

namespace MillionApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{

    private readonly IMediator _mediator;

    public PropertyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAllProperties()
    {
        var query = new GetAllPropertiesQuery();
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyDto>> GetPropertyById(Guid id)
    {
        var query = new GetPropertyByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<PropertyDto>> CreateProperty([FromBody] CreatePropertyCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetPropertyById), new { id = result.Value.PropertyId }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProperty(Guid id, [FromBody] UpdatePropertyCommand command)
    {
        if (id != command.PropertyId)
            return BadRequest("Property ID mismatch");

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProperty(Guid id)
    {
        var command = new DeletePropertyCommand(id);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }


}
