﻿using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class UpdatePropertyCommand : IRequest<Result<PropertyDto>>
{
    public Guid PropertyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Price { get; set; }
    public string CodeInternal { get; set; } = string.Empty;
    public int Year { get; set; }
    public Guid OwnerId { get; set; }
}

public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Result<PropertyDto>>
{
    private readonly IPropertyRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePropertyCommandHandler(IPropertyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyDto>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
    {
        var propertyResult = await _repository.GetByIdAsync(request.PropertyId);
        if (propertyResult.IsFailure)
            return Result.Failure<PropertyDto>(propertyResult.Error);

        var property = propertyResult.Value;

        var updateResult = property.Update(
            request.Name,
            request.Address,
            request.Price,
            request.CodeInternal,
            request.Year,
            request.OwnerId
        );

        if (updateResult.IsFailure)
            return Result.Failure<PropertyDto>(updateResult.Error);

        var updateSave = await _repository.UpdateAsync(property);
        if (updateSave.IsFailure)
            return Result.Failure<PropertyDto>(updateSave.Error);

        var dto = _mapper.Map<PropertyDto>(updateSave.Value);
        return Result.Success(dto);
    }
}


