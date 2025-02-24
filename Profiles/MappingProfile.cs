using AutoMapper;
using UserApi.DTOs;
using UserApi.Models;

namespace UserApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserReadDto>();

        CreateMap<ArchiveCreateDto, Archive>();
        CreateMap<Archive, ArchiveReadDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
    }
}