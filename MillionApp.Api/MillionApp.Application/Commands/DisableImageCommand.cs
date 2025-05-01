using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class DisableImageCommand : IRequest<Result<bool>>
{
    public Guid PropertyImageId { get; set; }

    public class DisableImageCommandHandler : IRequestHandler<DisableImageCommand, Result<bool>>
    {
        private readonly IPropertyImageRepository _repository;

        public DisableImageCommandHandler(IPropertyImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(DisableImageCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.PropertyImageId);
            if (result.IsFailure)
                return Result.Failure<bool>("Image not found");

            result.Value.Disable();
            return await _repository.UpdateAsync(result.Value).Map(_ => true);
        }
    }
}
