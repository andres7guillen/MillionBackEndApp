using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class AddPropertyImageCommand : IRequest<Result<PropertyImageDto>>
{
    public Guid PropertyId { get; set; }
    public string File { get; set; }
    public bool Enabled { get; set; }
}

public class AddPropertyImageCommandHandler : IRequestHandler<AddPropertyImageCommand, Result<PropertyImageDto>>
{
    private readonly IPropertyImageRepository _repository;
    private readonly IMapper _mapper;

    public AddPropertyImageCommandHandler(IPropertyImageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyImageDto>> Handle(AddPropertyImageCommand request, CancellationToken cancellationToken)
    {
        var createResult = PropertyImage.CreatePropertyImage(request.PropertyId, request.File, request.Enabled);
        if (createResult.IsFailure)
            return Result.Failure<PropertyImageDto>(createResult.Error);

        var propertyImage = createResult.Value;

        var addResult = await _repository.AddAsync(propertyImage);
        if (addResult.IsFailure)
            return Result.Failure<PropertyImageDto>(addResult.Error);

        var propertyImageDto = _mapper.Map<PropertyImageDto>(addResult.Value);
        return Result.Success(propertyImageDto);
    }
}



