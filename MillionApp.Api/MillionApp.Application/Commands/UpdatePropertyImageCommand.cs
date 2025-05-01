using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class UpdatePropertyImageCommand : IRequest<Result<PropertyImageDto>>
{
    public Guid Id { get; set; }
    public PropertyImageDto ImageDto { get; set; }
}
public class UpdatePropertyImageCommandHandler : IRequestHandler<UpdatePropertyImageCommand, Result<PropertyImageDto>>
{
    private readonly IPropertyImageRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePropertyImageCommandHandler(IPropertyImageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyImageDto>> Handle(UpdatePropertyImageCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id);
        if (existing.IsFailure)
            return Result.Failure<PropertyImageDto>(existing.Error);

        var updateResult = existing.Value.Update(request.ImageDto.File, request.ImageDto.Enabled);
        if (updateResult.IsFailure)
            return Result.Failure<PropertyImageDto>(updateResult.Error);

        var updated = await _repository.UpdateAsync(existing.Value);
        if (updated.IsFailure)
            return Result.Failure<PropertyImageDto>(updated.Error);

        return Result.Success(_mapper.Map<PropertyImageDto>(updated.Value));
    }
}
