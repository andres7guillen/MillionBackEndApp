using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Queries;

public class GetPropertyByIdQuery : IRequest<Result<PropertyDto>>
{
    public Guid Id { get; }

    public GetPropertyByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, Result<PropertyDto>>
{
    private readonly IPropertyRepository _repository;
    private readonly IMapper _mapper;

    public GetPropertyByIdQueryHandler(IPropertyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyDto>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id);
        if (result.IsFailure)
            return Result.Failure<PropertyDto>(result.Error);

        var dto = _mapper.Map<PropertyDto>(result.Value);
        return Result.Success(dto);
    }
}


