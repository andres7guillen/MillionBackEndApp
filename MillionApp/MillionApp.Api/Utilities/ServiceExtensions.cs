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

            // Comandos
            typeof(AddPropertyImageCommand).Assembly,
            typeof(ChangePriceCommand).Assembly,
            typeof(CreateOwnerCommand).Assembly,
            typeof(CreatePropertyCommand).Assembly,
            typeof(CreatePropertyTraceCommand).Assembly,
            typeof(DeleteOwnerCommand).Assembly,
            typeof(DeletePropertyCommand).Assembly,
            typeof(DeletePropertyImageCommand).Assembly,
            typeof(DeletePropertyTraceCommand).Assembly,
            typeof(DisableImageCommand).Assembly,
            typeof(UpdateOwnerCommand).Assembly,
            typeof(UpdatePropertyCommand).Assembly,            
            typeof(UpdatePropertyImageCommand).Assembly,
            typeof(UpdatePropertyTraceCommand).Assembly,

            // Queries
            typeof(GetAllPropertiesQuery).Assembly,
            typeof(GetPropertyByIdQuery).Assembly,
            typeof(GetAllOwnersQuery).Assembly,
            typeof(GetOwnerByIdQuery).Assembly,
            typeof(GetAllPropertyImagesQuery).Assembly,
            typeof(GetPropertyImageByIdQuery).Assembly,
            typeof(GetAllPropertyTracesQuery).Assembly,
            typeof(GetPropertyTraceByIdQuery).Assembly,
            typeof(GetPropertiesByFilterQuery).Assembly
        ));

        return services;
    }
}
