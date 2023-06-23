using AutoMapper;
using HumanResourceApi.DTO.Experience;
using HumanResourceApi.DTO.Job;
using HumanResourceApi.DTO.Leave;
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
            CreateMap<Experience, ExperienceDto>()
                .ReverseMap();
            CreateMap<Experience, UpdateExperienceDto>()
                .ReverseMap();
            CreateMap<Leave, LeaveDto>()
                .ReverseMap();
            CreateMap<Leave, UpdateLeaveDto>()
                .ReverseMap();
            CreateMap<Job, JobDto>()
                .ReverseMap();
            CreateMap<Job, UpdateJobDto>()
                .ReverseMap();
        }
    }
}
