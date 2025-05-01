using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionApp.Application.Commands;

public class DeletePropertyImageCommand : IRequest<Result<bool>>
{
    public Guid PropertyImageId { get; }

    public DeletePropertyImageCommand(Guid propertyImageId)
    {
        PropertyImageId = propertyImageId;
    }
}

public class DeletePropertyImageCommandHandler : IRequestHandler<DeletePropertyImageCommand, Result<bool>>
{
    private readonly IPropertyImageRepository _repository;

    public DeletePropertyImageCommandHandler(IPropertyImageRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(DeletePropertyImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _repository.GetByIdAsync(request.PropertyImageId);
        if (image.Value is null)
            return Result.Failure<bool>("Property image not found");

        await _repository.DeleteAsync(image.Value);
        return Result.Success(true);
    }
}
