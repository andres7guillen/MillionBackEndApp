using CSharpFunctionalExtensions;
using MillionApp.Domain.Entities;

namespace MillionApp.Domain.Repositories;

public interface IPropertyImageRepository
{
    Task<Result<PropertyImage>> AddAsync(PropertyImage image);
    Task<Result<PropertyImage>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<PropertyImage>>> GetAllAsync();
    Task<Result<PropertyImage>> UpdateAsync(PropertyImage image);
    Task<Result<bool>> DeleteAsync(PropertyImage image);
}


