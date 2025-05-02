using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class DeletePropertyCommand : IRequest<Result<bool>>
{
    public Guid PropertyId { get; set; }

    public DeletePropertyCommand(Guid propertyId)
    {
        PropertyId = propertyId;
    }
}

public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Result<bool>>
{
    private readonly IPropertyRepository _repository;

    public DeletePropertyCommandHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.PropertyId);
        if (result.IsFailure)
            return Result.Failure<bool>("Property not found");

        return await _repository.DeleteAsync(result.Value);
    }
}

