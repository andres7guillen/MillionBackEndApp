using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class CreateTraceCommand : IRequest<Result<PropertyTraceDto>>
{
    public PropertyTraceDto TraceDto { get; set; }

    public class CreateTraceCommandHandler : IRequestHandler<CreateTraceCommand, Result<PropertyTraceDto>>
    {
        private readonly IPropertyTraceRepository _repository;
        private readonly IMapper _mapper;

        public CreateTraceCommandHandler(IPropertyTraceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PropertyTraceDto>> Handle(CreateTraceCommand request, CancellationToken cancellationToken)
        {
            var result = PropertyTrace.CreatePropertyTrace(request.TraceDto.DateSale, request.TraceDto.Name, request.TraceDto.Value, request.TraceDto.Tax, request.TraceDto.PropertyId);
            if (result.IsFailure)
                return Result.Failure<PropertyTraceDto>(result.Error);

            var saved = await _repository.AddAsync(result.Value);
            if (saved.IsFailure)
                return Result.Failure<PropertyTraceDto>(saved.Error);

            return Result.Success(_mapper.Map<PropertyTraceDto>(saved.Value));
        }
    }
}
