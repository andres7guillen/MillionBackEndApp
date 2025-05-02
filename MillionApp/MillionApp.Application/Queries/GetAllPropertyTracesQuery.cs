using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Queries;

public class GetAllPropertyTracesQuery : IRequest<Result<IEnumerable<PropertyTraceDto>>> { }

public class GetAllPropertyTracesQueryHandler : IRequestHandler<GetAllPropertyTracesQuery, Result<IEnumerable<PropertyTraceDto>>>
{
    private readonly IPropertyTraceRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPropertyTracesQueryHandler(IPropertyTraceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<PropertyTraceDto>>> Handle(GetAllPropertyTracesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync();
        if (result.IsFailure)
            return Result.Failure<IEnumerable<PropertyTraceDto>>(result.Error);

        var dtoList = _mapper.Map<IEnumerable<PropertyTraceDto>>(result.Value);
        return Result.Success(dtoList);
    }
}


