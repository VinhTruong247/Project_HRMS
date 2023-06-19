using AutoMapper;
using HumanResourceApi.DTO.Users;
using HumanResourceApi.Models;

namespace HumanResourceApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
            CreateMap<User, UpdateUserDto>()
                .ReverseMap();
        }
    }
}
