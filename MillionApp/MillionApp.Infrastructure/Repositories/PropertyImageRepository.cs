using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MillionApp.Data.Context;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;

namespace MillionApp.Infrastructure.Repositories;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly ApplicationDbContext _context;

    public PropertyImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PropertyImage>> AddAsync(PropertyImage image)
    {
        await _context.PropertyImages.AddAsync(image);
        await _context.SaveChangesAsync();
        return Result.Success(image);
    }

    public async Task<Result<PropertyImage>> GetByIdAsync(Guid id)
    {
        var image = await _context.PropertyImages.FindAsync(id);
        return image is not null
            ? Result.Success(image)
            : Result.Failure<PropertyImage>("Image not found.");
    }

    public async Task<Result<IEnumerable<PropertyImage>>> GetAllAsync()
    {
        var images = await _context.PropertyImages.ToListAsync();
        return Result.Success<IEnumerable<PropertyImage>>(images);
    }

    public async Task<Result<PropertyImage>> UpdateAsync(PropertyImage image)
    {
        _context.PropertyImages.Update(image);
        await _context.SaveChangesAsync();
        return Result.Success(image);
    }

    public async Task<Result<bool>> DeleteAsync(PropertyImage image)
    {
        _context.PropertyImages.Remove(image);
        return await _context.SaveChangesAsync() > 0
            ? Result.Success(true)
            : Result.Failure<bool>("Error removind the property image");
    }
}

