using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Queries;

public class GetPropertyImageByIdQuery : IRequest<Result<PropertyImageDto>>
{
    public Guid Id { get; }

    public GetPropertyImageByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetPropertyImageByIdQueryHandler : IRequestHandler<GetPropertyImageByIdQuery, Result<PropertyImageDto>>
{
    private readonly IPropertyImageRepository _repository;
    private readonly IMapper _mapper;

    public GetPropertyImageByIdQueryHandler(IPropertyImageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyImageDto>> Handle(GetPropertyImageByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id);
        if (result.IsFailure)
            return Result.Failure<PropertyImageDto>(result.Error);

        var dto = _mapper.Map<PropertyImageDto>(result.Value);
        return Result.Success(dto);
    }
}
