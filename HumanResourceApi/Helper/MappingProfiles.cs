using AutoMapper;
using HumanResourceApi.DTO.Allowance;
using HumanResourceApi.DTO.Attendance;
using HumanResourceApi.DTO.Department;
using HumanResourceApi.DTO.Employee;
using HumanResourceApi.DTO.EmployeeContract;
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
            CreateMap<Allowance, AllowanceDto>()
                .ReverseMap();
            CreateMap<Allowance, UpdateAllowanceDto>()
                .ReverseMap();
            CreateMap<Attendance, AttendanceDto>()
                .ReverseMap();
            CreateMap<Attendance, UpdateAttendanceDto>()
                .ReverseMap();
            CreateMap<Department, DepartmentDto>()
                .ReverseMap();
            CreateMap<Department, UpdateDepartmentDto>()
                .ReverseMap();
            CreateMap<EmployeeContract, EmployeeContractDto>()
                .ReverseMap();
            CreateMap<EmployeeContract, UpdateEmployeeContractDto>()
                .ReverseMap();
            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>()
                .ReverseMap();
        }
    }
}
