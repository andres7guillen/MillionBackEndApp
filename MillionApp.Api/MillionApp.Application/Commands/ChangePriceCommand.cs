using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class ChangePriceCommand : IRequest<Result<PropertyDto>>
{
    public Guid PropertyId { get; set; }
    public double NewPrice { get; set; }
}

public class Handler : IRequestHandler<ChangePriceCommand, Result<PropertyDto>>
{
    private readonly IPropertyRepository _repository;
    private readonly IMapper _mapper;

    public Handler(IPropertyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyDto>> Handle(ChangePriceCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.PropertyId);
        if (result.IsFailure)
            return Result.Failure<PropertyDto>(result.Error);

        var property = result.Value;
        var priceResult = property.ChangePrice(request.NewPrice);
        if (priceResult.IsFailure)
            return Result.Failure<PropertyDto>(priceResult.Error);

        var updateResult = await _repository.UpdateAsync(property);
        if (updateResult.IsFailure)
            return Result.Failure<PropertyDto>(updateResult.Error);

        return Result.Success(_mapper.Map<PropertyDto>(updateResult.Value));
    }
}




