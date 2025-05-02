namespace MillionApp.Application.Utilities;

using AutoMapper;
using MillionApp.Domain.Dtos;
using MillionApp.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Property, PropertyDto>().ReverseMap();
        CreateMap<Owner, OwnerDto>().ReverseMap();
        CreateMap<PropertyImage, PropertyImageDto>().ReverseMap();
        CreateMap<PropertyTrace, PropertyTraceDto>().ReverseMap();
    }
}

