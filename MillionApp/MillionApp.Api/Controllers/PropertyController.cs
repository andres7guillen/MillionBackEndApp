using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MillionApp.Api.Middleware;
using MillionApp.Api.Models.Security;
using MillionApp.Application.Commands;
using MillionApp.Application.Queries;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Exceptions;

namespace MillionApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PropertyController : ControllerBase
{

    private readonly IMediator _mediator;

    public PropertyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomResponse<object>))]
    public async Task<IActionResult> GetAllProperties()
    {
        var query = new GetAllPropertiesQuery();
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.NoPropertiesFound);
            var errorResponse = new ErrorResponse(code, message);
            return StatusCode(500, CustomResponse<ErrorResponse>.BuildError(500, errorResponse.Message));
        }

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<object>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomResponse<object>))]
    public async Task<IActionResult> GetPropertyById(Guid id)
    {
        var query = new GetPropertyByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.NoPropertiesFound);
            var errorResponse = new ErrorResponse(code, message);
            return StatusCode(404, CustomResponse<ErrorResponse>.BuildError(404, errorResponse.Message));
        }
        return Ok(result.Value);
    }

    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<object>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomResponse<object>))]
    public async Task<IActionResult> GetFilteredProperties(
        [FromQuery] string? name,
        [FromQuery] string? address,
        [FromQuery] double? price,
        [FromQuery] string? codeInternal,
        [FromQuery] int? year,
        [FromQuery] Guid? ownerId)
    {
        var query = new GetPropertiesByFilterQuery
        {
            Name = name,
            Address = address,
            Price = price,
            CodeInternal = codeInternal,
            Year = year,
            OwnerId = ownerId
        };

        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(CustomResponse<object>.BuildError(404, result.Error));
        }

        return Ok(CustomResponse<object>.BuildSuccess(result.Value));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomResponse<object>))]
    public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.ErrorCreatingProperty);
            var errorResponse = new ErrorResponse(code, message);
            return StatusCode(500, CustomResponse<ErrorResponse>.BuildError(500, errorResponse.Message));
        }

        return Ok(result.Value);
    }

    [HttpPut("{id}/update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomResponse<object>))]
    public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] UpdatePropertyCommand command)
    {
        if (id != command.PropertyId)
        {
            var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.PropertyIdIsmatch);
            var errorResponse = new ErrorResponse(code, message);
            return StatusCode(500, CustomResponse<ErrorResponse>.BuildError(500, errorResponse.Message));
        }

        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.ErrorUpdatingProperty);
            var errorResponse = new ErrorResponse(code, message);
            return StatusCode(500, CustomResponse<ErrorResponse>.BuildError(500, errorResponse.Message));
        }

        return Ok(result.Value);
    }

    [HttpPut("{id}/change-price")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomResponse<object>))]
    public async Task<IActionResult> ChangePrice(Guid id, [FromBody] ChangePriceCommand command) 
    {
        if (id != command.PropertyId)
        {
            var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.PropertyIdIsmatch);
            var errorResponse = new ErrorResponse(code, message);
            return StatusCode(500, CustomResponse<ErrorResponse>.BuildError(500, errorResponse.Message));
        }

        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.ErrorChangingPrice);
            var errorResponse = new ErrorResponse(code, message);
            return StatusCode(500, CustomResponse<ErrorResponse>.BuildError(500, errorResponse.Message));
        }

        return Ok(result.Value);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(Guid id)
    {
        var command = new DeletePropertyCommand(id);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            var (code, message) = BusinessContextException.Detail(BusinessContextExceptionEnum.ErrorRemovingProperty);
            var errorResponse = new ErrorResponse(code, message);
            return StatusCode(500, CustomResponse<ErrorResponse>.BuildError(500, errorResponse.Message));
        }

        return NoContent();
    }


}
