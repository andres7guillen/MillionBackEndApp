using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Queries;

public class GetAllPropertyImagesQuery : IRequest<Result<IEnumerable<PropertyImageDto>>> { }

public class GetAllPropertyImagesQueryHandler : IRequestHandler<GetAllPropertyImagesQuery, Result<IEnumerable<PropertyImageDto>>>
{
    private readonly IPropertyImageRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPropertyImagesQueryHandler(IPropertyImageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<PropertyImageDto>>> Handle(GetAllPropertyImagesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync();
        if (result.IsFailure)
            return Result.Failure<IEnumerable<PropertyImageDto>>(result.Error);

        var dtoList = _mapper.Map<IEnumerable<PropertyImageDto>>(result.Value);
        return Result.Success(dtoList);
    }
}