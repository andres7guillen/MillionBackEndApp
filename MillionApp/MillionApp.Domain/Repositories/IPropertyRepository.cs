using CSharpFunctionalExtensions;
using MillionApp.Domain.Entities;

namespace MillionApp.Domain.Repositories;

public interface IPropertyRepository
{
    Task<Result<Property>> AddAsync(Property property);
    Task<Result<Property>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<Property>>> GetAllAsync();
    Task<Result<Property>> UpdateAsync(Property property);
    Task<Result<bool>> DeleteAsync(Property property);
}



