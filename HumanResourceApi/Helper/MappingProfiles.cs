using AutoMapper;
using HumanResourceApi.DTO;
using HumanResourceApi.Models;

namespace HumanResourceApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
        }
    }
}
