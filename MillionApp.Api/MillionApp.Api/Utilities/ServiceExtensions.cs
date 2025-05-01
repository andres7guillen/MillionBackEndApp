using MediatR;
using MillionApp.Application.Commands;
using MillionApp.Application.Queries;
using MillionApp.Application.Utilities;
using MillionApp.Domain.Entities;
using MillionApp.Domain.Repositories;
using MillionApp.Infrastructure.Repositories;
using System.Reflection;

namespace MillionApp.Api.Utilities;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        // Repositorios
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
        services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();

        services.AddAutoMapper(cfg =>
        { 
            cfg.AddProfile<MappingProfile>();
        });

        // MeddiatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly(),

            // Comandos existentes
            typeof(CreatePropertyCommand).Assembly,
            typeof(UpdatePropertyCommand).Assembly,
            typeof(ChangePriceCommand).Assembly,
            typeof(DeletePropertyCommand).Assembly,
            typeof(CreateOwnerCommand).Assembly,
            typeof(UpdateOwnerCommand).Assembly,
            typeof(DeleteOwnerCommand).Assembly,
            typeof(AddPropertyImageCommand).Assembly,
            typeof(UpdateImageCommand).Assembly,
            typeof(DisableImageCommand).Assembly,
            typeof(CreateTraceCommand).Assembly,

            // Queries existentes
            typeof(GetAllPropertiesQuery).Assembly,
            typeof(GetPropertyByIdQuery).Assembly,
            typeof(GetAllOwnersQuery).Assembly,
            typeof(GetOwnerByIdQuery).Assembly,
            typeof(GetAllPropertyImagesQuery).Assembly,
            typeof(GetPropertyImageByIdQuery).Assembly,
            typeof(GetAllPropertyTracesQuery).Assembly,
            typeof(GetPropertyTraceByIdQuery).Assembly
        ));

        return services;
    }
}
