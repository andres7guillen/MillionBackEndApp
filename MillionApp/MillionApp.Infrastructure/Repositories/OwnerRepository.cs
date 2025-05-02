using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MillionApp.Data.Context;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly ApplicationDbContext _context;

    public OwnerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Owner>> AddAsync(Owner owner)
    {
        await _context.Owners.AddAsync(owner);
        await _context.SaveChangesAsync();
        return Result.Success(owner);
    }

    public async Task<Result<Owner>> GetByIdAsync(Guid id)
    {
        var owner = await _context.Owners.FindAsync(id);
        return owner is not null
            ? Result.Success(owner)
            : Result.Failure<Owner>("Owner not found.");
    }

    public async Task<Result<IEnumerable<Owner>>> GetAllAsync()
    {
        var owners = await _context.Owners.ToListAsync();
        return Result.Success<IEnumerable<Owner>>(owners);
    }

    public async Task<Result<Owner>> UpdateAsync(Owner owner)
    {
        _context.Owners.Update(owner);
        await _context.SaveChangesAsync();
        return Result.Success(owner);
    }

    public async Task<Result<bool>> DeleteAsync(Owner owner)
    {
        _context.Owners.Remove(owner);
        return await _context.SaveChangesAsync() > 0
         ? Result.Success(true)
         : Result.Failure<bool>("Error removing the owner");
    }
}

