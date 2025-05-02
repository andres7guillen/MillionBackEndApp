using MediatR;
using Microsoft.AspNetCore.Mvc;
using MillionApp.Application.Commands;
using MillionApp.Application.Queries;
using MillionApp.Domain.Dtos;

namespace MillionApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OwnerController : ControllerBase
{
    private readonly IMediator _mediator;

    public OwnerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<OwnerDto>> GetAll()
    {
        var query = new GetAllOwnersQuery();
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OwnerDto>> GetOwnerById(Guid id)
    {
        var query = new GetOwnerByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<OwnerDto>> CreateOwner([FromBody] CreateOwnerCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetOwnerById), new { id = result.Value.OwnerId }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOwner(Guid id, [FromBody] UpdateOwnerCommand command)
    {
        if (id != command.Id)
            return BadRequest("Owner ID mismatch");

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOwner(Guid id)
    {
        var command = new DeleteOwnerCommand(id);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}
