using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MillionApp.Application.Commands;
using MillionApp.Application.Queries;
using MillionApp.Domain.Dtos;

namespace MillionApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropertyImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/propertyimage
        [HttpPost]
        public async Task<ActionResult<PropertyImageDto>> AddImage([FromBody] AddPropertyImageCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.PropertyImageId }, result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPropertyImageByIdQuery(id));

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateImage(Guid id, [FromBody] UpdatePropertyImageCommand command)
        {
            if (id != command.Id)
                return BadRequest("Image ID mismatch");

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteImage(Guid id)
        {
            var command = new DeletePropertyImageCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
