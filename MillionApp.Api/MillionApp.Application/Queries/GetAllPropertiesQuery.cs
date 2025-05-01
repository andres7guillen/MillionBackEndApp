using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Queries;

public class GetAllPropertiesQuery : IRequest<Result<IEnumerable<PropertyDto>>>
{}
public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, Result<IEnumerable<PropertyDto>>>
{
    private readonly IPropertyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPropertiesQueryHandler(IPropertyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<PropertyDto>>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync();
        if (result.IsFailure)
            return Result.Failure<IEnumerable<PropertyDto>>(result.Error);

        var dtoList = _mapper.Map<IEnumerable<PropertyDto>>(result.Value);
        return Result.Success(dtoList);
    }
}
