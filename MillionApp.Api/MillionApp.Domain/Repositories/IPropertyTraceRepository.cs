using CSharpFunctionalExtensions;
using MillionApp.Domain.Entities;

namespace MillionApp.Domain.Repositories;

public interface IPropertyTraceRepository
{
    Task<Result<PropertyTrace>> AddAsync(PropertyTrace trace);
    Task<Result<PropertyTrace>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<PropertyTrace>>> GetAllAsync();
    Task<Result<PropertyTrace>> UpdateAsync(PropertyTrace trace);
    Task<Result<bool>> DeleteAsync(PropertyTrace trace);
}
