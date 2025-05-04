using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MillionApp.Application.Commands;
using MillionApp.Application.Queries;
using MillionApp.Domain.Dtos;

namespace MillionApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropertyTraceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyTraceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPropertyTraceByIdQuery(id));

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        // POST api/propertytrace
        [HttpPost]
        public async Task<ActionResult<PropertyTraceDto>> AddTrace([FromBody] CreatePropertyTraceCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.PropertyTraceId }, result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPropertyTracesQuery());

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        // PUT api/propertytrace/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTrace(Guid id, [FromBody] UpdatePropertyTraceCommand command)
        {
            if (id != command.PropertyTraceId)
                return BadRequest("Trace ID mismatch");

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        // DELETE api/propertytrace/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrace(Guid id)
        {
            var command = new DeletePropertyTraceCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
