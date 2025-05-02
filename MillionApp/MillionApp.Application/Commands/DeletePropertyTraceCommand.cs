using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class DeletePropertyTraceCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }

    public DeletePropertyTraceCommand(Guid id)
    {
        Id = id;
    }
}

public class DeletePropertyTraceCommandHandler : IRequestHandler<DeletePropertyTraceCommand, Result<bool>>
{
    private readonly IPropertyTraceRepository _repository;

    public DeletePropertyTraceCommandHandler(IPropertyTraceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(DeletePropertyTraceCommand request, CancellationToken cancellationToken)
    {
        var propertyTrace = await _repository.GetByIdAsync(request.Id);
        if (propertyTrace.Value is null)
            return Result.Failure<bool>("PropertyTrace not found");

        var result = await _repository.DeleteAsync(propertyTrace.Value);
        return result;
    }
}
