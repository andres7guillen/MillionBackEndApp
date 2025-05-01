using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Queries;
public class GetPropertyTraceByIdQuery : IRequest<Result<PropertyTraceDto>>
{
    public Guid Id { get; }

    public GetPropertyTraceByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetPropertyTraceByIdQueryHandler : IRequestHandler<GetPropertyTraceByIdQuery, Result<PropertyTraceDto>>
{
    private readonly IPropertyTraceRepository _repository;
    private readonly IMapper _mapper;

    public GetPropertyTraceByIdQueryHandler(IPropertyTraceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyTraceDto>> Handle(GetPropertyTraceByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id);
        if (result.IsFailure)
            return Result.Failure<PropertyTraceDto>(result.Error);

        var dto = _mapper.Map<PropertyTraceDto>(result.Value);
        return Result.Success(dto);
    }
}

