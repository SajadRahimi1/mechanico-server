using AutoMapper;
using Mechanico_Api.Dtos;
using Mechanico_Api.Entities;

namespace Mechanico_Api.Contexts;

public class MapperProfiles:Profile
{
    public MapperProfiles()
    {
        CreateMap<UpdateUserDto, User>();
        CreateMap<UpdateMechanicDto, Mechanic>();
    }
}