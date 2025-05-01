using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Repositories;

namespace MillionApp.Application.Commands;

public class UpdatePropertyTraceCommand : IRequest<Result<PropertyTraceDto>>
{
    public Guid PropertyTraceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public DateTime DateSale { get; set; }
    public double Tax { get; set; }
    public string Total { get; set; } = string.Empty;
}

public sealed class UpdatePropertyTraceCommandHandler : IRequestHandler<UpdatePropertyTraceCommand, Result<PropertyTraceDto>>
{
    private readonly IPropertyTraceRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePropertyTraceCommandHandler(IPropertyTraceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyTraceDto>> Handle(UpdatePropertyTraceCommand request, CancellationToken cancellationToken)
    {
        var trace = await _repository.GetByIdAsync(request.PropertyTraceId);
        if (trace.Value == null)
            return Result.Failure<PropertyTraceDto>("Trace not found");

        var updateResult = trace.Value.Update(request.DateSale, request.Name, request.Value, request.Tax);
        if (updateResult.IsFailure)
            return Result.Failure<PropertyTraceDto>(updateResult.Error);

        await _repository.UpdateAsync(trace.Value);

        var dto = _mapper.Map<PropertyTraceDto>(trace);
        return Result.Success(dto);
    }
}
