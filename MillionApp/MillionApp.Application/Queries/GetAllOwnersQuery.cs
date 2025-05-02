using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Queries;

public class GetAllOwnersQuery : IRequest<Result<IEnumerable<OwnerDto>>> { }

public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, Result<IEnumerable<OwnerDto>>>
{
    private readonly IOwnerRepository _repository;
    private readonly IMapper _mapper;

    public GetAllOwnersQueryHandler(IOwnerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<OwnerDto>>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync();
        if (result.IsFailure)
            return Result.Failure<IEnumerable<OwnerDto>>(result.Error);

        var dtoList = _mapper.Map<IEnumerable<OwnerDto>>(result.Value);
        return Result.Success(dtoList);
    }
}



