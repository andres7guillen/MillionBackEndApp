using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class UpdateImageCommand : IRequest<Result<PropertyImageDto>>
{
    public Guid Id { get; set; }
    public PropertyImageDto ImageDto { get; set; }
}
public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, Result<PropertyImageDto>>
{
    private readonly IPropertyImageRepository _repository;
    private readonly IMapper _mapper;

    public UpdateImageCommandHandler(IPropertyImageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyImageDto>> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
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
