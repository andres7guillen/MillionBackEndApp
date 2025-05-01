using CSharpFunctionalExtensions;
using MillionApp.Domain.Entities;

namespace MillionApp.Domain.Repositories;

public interface IOwnerRepository
{
    Task<Result<Owner>> AddAsync(Owner owner);
    Task<Result<Owner>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<Owner>>> GetAllAsync();
    Task<Result<Owner>> UpdateAsync(Owner owner);
    Task<Result<bool>> DeleteAsync(Owner owner);
}


