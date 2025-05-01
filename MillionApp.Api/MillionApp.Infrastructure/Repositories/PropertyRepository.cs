using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MillionApp.Data.Context;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly ApplicationDbContext _context;

    public PropertyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Property>> AddAsync(Property property)
    {
        await _context.Properties.AddAsync(property);
        await _context.SaveChangesAsync();
        return Result.Success(property);
    }

    public async Task<Result<Property>> GetByIdAsync(Guid id)
    {
        var property = await _context.Properties.FindAsync(id);
        return property is not null
            ? Result.Success(property)
            : Result.Failure<Property>("Property not found.");
    }

    public async Task<Result<IEnumerable<Property>>> GetAllAsync()
    {
        var properties = await _context.Properties.ToListAsync();
        return Result.Success<IEnumerable<Property>>(properties);
    }

    public async Task<Result<Property>> UpdateAsync(Property property)
    {
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
        return Result.Success(property);
    }

    public async Task<Result<bool>> DeleteAsync(Property property)
    {
        _context.Properties.Remove(property);
        return await _context.SaveChangesAsync() > 0
            ? Result.Success(true)
            : Result.Failure<bool>("Error removing the property");
    }
}

