using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class UpdateOwnerCommand : IRequest<Result<OwnerDto>>
{
    public Guid Id { get; set; }
    public OwnerDto OwnerDto { get; set; }
}

public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, Result<OwnerDto>>
{
    private readonly IOwnerRepository _repository;
    private readonly IMapper _mapper;

    public UpdateOwnerCommandHandler(IOwnerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<OwnerDto>> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id);
        if (existing.IsFailure)
            return Result.Failure<OwnerDto>(existing.Error);

        var updateResult = existing.Value.Update(request.OwnerDto.Name, request.OwnerDto.Address, request.OwnerDto.Photo, request.OwnerDto.Birthday);
        if (updateResult.IsFailure)
            return Result.Failure<OwnerDto>(updateResult.Error);

        var updated = await _repository.UpdateAsync(existing.Value);
        if (updated.IsFailure)
            return Result.Failure<OwnerDto>(updated.Error);

        return Result.Success(_mapper.Map<OwnerDto>(updated.Value));
    }
}
