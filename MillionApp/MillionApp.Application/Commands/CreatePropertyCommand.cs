using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class CreatePropertyCommand : IRequest<Result<PropertyDto>>
{
    public PropertyDto Property { get; set; }
}

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Result<PropertyDto>>
{
    private readonly IPropertyRepository _repository;
    private readonly IMapper _mapper;

    public CreatePropertyCommandHandler(IPropertyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyDto>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = Property.CreateProperty(
            request.Property.Name,
            request.Property.Address,
            request.Property.Price,
            request.Property.CodeInternal,
            request.Property.Year,
            request.Property.OwnerId
        );

        if (property.IsFailure)
            return Result.Failure<PropertyDto>(property.Error);

        var saveResult = await _repository.AddAsync(property.Value);
        if (saveResult.IsFailure)
            return Result.Failure<PropertyDto>(saveResult.Error);

        var dto = _mapper.Map<PropertyDto>(saveResult.Value);
        return Result.Success(dto);
    }
}





