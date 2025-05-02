using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class DeleteOwnerCommand : IRequest<Result<bool>>
{
    public Guid OwnerId { get; set; }

    public DeleteOwnerCommand(Guid ownerId)
    {
        OwnerId = ownerId;
    }
}

public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand, Result<bool>>
{
    private readonly IOwnerRepository _repository;

    public DeleteOwnerCommandHandler(IOwnerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.OwnerId);
        if (result.IsFailure)
            return Result.Failure<bool>("Owner not found");

        return await _repository.DeleteAsync(result.Value);
    }
}
