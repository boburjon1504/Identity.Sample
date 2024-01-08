using AutoMapper;
using Identity.Application.Common.Identity.Models;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Common.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, RegisterDetails>().ReverseMap();
    }
}