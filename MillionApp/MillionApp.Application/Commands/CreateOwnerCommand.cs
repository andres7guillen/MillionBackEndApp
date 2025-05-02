using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionApp.Application.Commands;

public class CreateOwnerCommand : IRequest<Result<OwnerDto>>
{
    public OwnerDto OwnerDto { get; set; }   
}

public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Result<OwnerDto>>
{
    private readonly IOwnerRepository _repository;
    private readonly IMapper _mapper;

    public CreateOwnerCommandHandler(IOwnerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<OwnerDto>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {
        var result = Owner.CreateOwner(request.OwnerDto.Name, request.OwnerDto.Address, request.OwnerDto.Photo, request.OwnerDto.Birthday);
        if (result.IsFailure)
            return Result.Failure<OwnerDto>(result.Error);

        var saveResult = await _repository.AddAsync(result.Value);
        if (saveResult.IsFailure)
            return Result.Failure<OwnerDto>(saveResult.Error);

        return Result.Success(_mapper.Map<OwnerDto>(saveResult.Value));
    }
}
