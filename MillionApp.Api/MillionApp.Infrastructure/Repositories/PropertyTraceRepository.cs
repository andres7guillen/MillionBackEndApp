using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MillionApp.Data.Context;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Infrastructure.Repositories;

public class PropertyTraceRepository : IPropertyTraceRepository
{
    private readonly ApplicationDbContext _context;

    public PropertyTraceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PropertyTrace>> AddAsync(PropertyTrace trace)
    {
        await _context.PropertyTraces.AddAsync(trace);
        await _context.SaveChangesAsync();
        return Result.Success(trace);
    }

    public async Task<Result<PropertyTrace>> GetByIdAsync(Guid id)
    {
        var trace = await _context.PropertyTraces.FindAsync(id);
        return trace is not null
            ? Result.Success(trace)
            : Result.Failure<PropertyTrace>("Property trace not found.");
    }

    public async Task<Result<IEnumerable<PropertyTrace>>> GetAllAsync()
    {
        var traces = await _context.PropertyTraces.ToListAsync();
        return Result.Success<IEnumerable<PropertyTrace>>(traces);
    }

    public async Task<Result<PropertyTrace>> UpdateAsync(PropertyTrace trace)
    {
        _context.PropertyTraces.Update(trace);
        await _context.SaveChangesAsync();
        return Result.Success(trace);
    }

    public async Task<Result<bool>> DeleteAsync(PropertyTrace trace)
    {
        _context.PropertyTraces.Remove(trace);
        return await _context.SaveChangesAsync() > 0
            ? Result.Success(true)
            : Result.Failure<bool>("Error removing the propertyTrace");
    }
}

