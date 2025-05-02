using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Queries;

public class GetOwnerByIdQuery : IRequest<Result<OwnerDto>>
{
    public Guid Id { get; }

    public GetOwnerByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, Result<OwnerDto>>
{
    private readonly IOwnerRepository _repository;
    private readonly IMapper _mapper;

    public GetOwnerByIdQueryHandler(IOwnerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<OwnerDto>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id);
        if (result.IsFailure)
            return Result.Failure<OwnerDto>(result.Error);

        var dto = _mapper.Map<OwnerDto>(result.Value);
        return Result.Success(dto);
    }
}
