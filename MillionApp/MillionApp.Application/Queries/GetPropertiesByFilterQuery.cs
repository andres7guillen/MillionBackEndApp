using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionApp.Application.Queries;
//GetPropertiesByFilterQuery
public class GetPropertiesByFilterQuery : IRequest<Result<List<PropertyDto>>> 
{
    public string? Name { get; init; }
    public string? Address { get; init; }
    public double? Price { get; init; }
    public string? CodeInternal { get; init; }
    public int? Year { get; init; }
    public Guid? OwnerId { get; init; }
}

public class GetPropertiesByFilterQueryHandler : IRequestHandler<GetPropertiesByFilterQuery, Result<List<PropertyDto>>>
{
    private readonly IPropertyRepository _repository;
    private readonly IMapper _mapper;

    public GetPropertiesByFilterQueryHandler(IPropertyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<List<PropertyDto>>> Handle(GetPropertiesByFilterQuery request, CancellationToken cancellationToken)
    {
        var allResult = await _repository.GetAllAsync();
        if (allResult.IsFailure)
            return Result.Failure<List<PropertyDto>>(allResult.Error);

        var properties = allResult.Value.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
            properties = properties.Where(p => p.Name.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Address))
            properties = properties.Where(p => p.Address.Contains(request.Address));

        if (request.Price.HasValue)
            properties = properties.Where(p => p.Price == request.Price.Value);

        if (!string.IsNullOrWhiteSpace(request.CodeInternal))
            properties = properties.Where(p => p.CodeInternal.Contains(request.CodeInternal));

        if (request.Year.HasValue)
            properties = properties.Where(p => p.Year == request.Year.Value);

        if (request.OwnerId.HasValue)
            properties = properties.Where(p => p.OwnerId == request.OwnerId.Value);

        var dtoList = _mapper.Map<List<PropertyDto>>(properties);
        return Result.Success(dtoList);
    }
}


