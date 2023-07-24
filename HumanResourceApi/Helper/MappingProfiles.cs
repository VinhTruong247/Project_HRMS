using AutoMapper;
using HumanResourceApi.DTO.Allowance;
using HumanResourceApi.DTO.Attendance;
using HumanResourceApi.DTO.DailySalary;
using HumanResourceApi.DTO.Department;
using HumanResourceApi.DTO.DepartmentMemberList;
using HumanResourceApi.DTO.Employee;
using HumanResourceApi.DTO.EmployeeBenefit;
using HumanResourceApi.DTO.EmployeeContract;
using HumanResourceApi.DTO.Experience;
using HumanResourceApi.DTO.Job;
using HumanResourceApi.DTO.Leave;
using HumanResourceApi.DTO.Overtime;
using HumanResourceApi.DTO.PaySlip;
using HumanResourceApi.DTO.Project;
using HumanResourceApi.DTO.Report;
using HumanResourceApi.DTO.Skill;
using HumanResourceApi.DTO.SkillEmployee;
using HumanResourceApi.DTO.Timesheet;
using HumanResourceApi.DTO.Users;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;

namespace HumanResourceApi.Helper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            AllowanceMap();
            AttendaceMap();
            DepartmentMap();
            DepartmentMemberListMap();
            EmployeeContractMap();
            EmployeeMap();
            ExperienceMap();
            JobMap();
            LeaveMap();
            ProjectMap();
            SkillEmployeeMap();
            SkillMap();
            UserMap();
            EmployeeBenefitMap();
            PaySlipMap();
            ReportMap();
            OvertimeMap();
            TimesheetMap();
            DailySalaryMap();
        }
        private void DailySalaryMap()
        {
            CreateMap<ResponseDailySalary, DailySalary>()
                .ReverseMap();
            CreateMap<RequestDailySalary, DailySalary>()
                .ReverseMap();
            CreateMap<DailyAllowance, Allowance>()
                .ReverseMap();
        }

        private void ProjectMap()
        {
            CreateMap<Project, ProjectDto>()
                .ReverseMap();
            CreateMap<Project, UpdateProjectDto>()
                .ReverseMap();
            CreateMap<Project, ResponseProjectDto>()
                .ReverseMap();
        }
        private void DepartmentMemberListMap()
        {
            CreateMap<DepartmentMemberList, DepartmentMemberDto>()
                .ReverseMap();
            CreateMap<DepartmentMemberList, UpdateDepartmentMemberDto>()
                .ReverseMap();
        }
        private void SkillEmployeeMap()
        {
            CreateMap<SkillEmployee, SkillEmployeeDto>()
                .ReverseMap();
            CreateMap<SkillEmployee, UpdateSkillEmployeeDto>()
                .ReverseMap();
        }
        private void SkillMap()
        {
            CreateMap<Skill, SkillDto>()
                .ReverseMap();
            CreateMap<Skill, UpdateSkillDto>()
                .ReverseMap();
        }
        private void EmployeeMap()
        {
            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>()
                .ReverseMap();
            CreateMap<Employee, UpdateProfileDto>()
                .ReverseMap();
            CreateMap<Employee, ResponseEmployeeDto>()
                .ReverseMap();
        }
        private void EmployeeContractMap()
        {
            CreateMap<EmployeeContract, EmployeeContractDto>()
                .ReverseMap();
            CreateMap<EmployeeContract, UpdateEmployeeContractDto>()
                .ReverseMap();
            CreateMap<EmployeeContract, ResponseEmployeeContractDto>()
                .ReverseMap();
        }
        private void DepartmentMap()
        {
            CreateMap<Department, DepartmentDto>()
                .ReverseMap();
            CreateMap<Department, UpdateDepartmentDto>()
                    .ReverseMap();
            CreateMap<Department, ResponseDepartmentDto>()
                    .ReverseMap();
        }
        private void AttendaceMap()
        {
            CreateMap<Attendance, AttendanceDto>()
                .ReverseMap();
            CreateMap<Attendance, UpdateAttendanceDto>()
                .ReverseMap();
        }
        private void AllowanceMap()
        {
            CreateMap<Allowance, AllowanceDto>()
                .ReverseMap();
            CreateMap<Allowance, UpdateAllowanceDto>()
                .ReverseMap();
            CreateMap<Allowance, ResponseAllowanceDto>()
                .ReverseMap();
        }
        private void JobMap()
        {
            CreateMap<Job, JobDto>()
                .ReverseMap();
            CreateMap<Job, UpdateJobDto>()
                .ReverseMap();
        }
        private void LeaveMap()
        {
            CreateMap<Leave, LeaveDto>()
                .ReverseMap();
            CreateMap<Leave, UpdateLeaveDto>()
                .ReverseMap();
            CreateMap<Leave, ResponseLeaveDto>()
                .ReverseMap();
        }
        private void ExperienceMap()
        {
            CreateMap<Experience, ExperienceDto>()
                .ReverseMap();
            CreateMap<Experience, UpdateExperienceDto>()
                .ReverseMap();
            CreateMap<Experience, ResponseExperienceDto>()
                .ReverseMap();
        }
        private void UserMap()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
            CreateMap<User, ResponseUserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<User, UpdateUserDto>()
                .ReverseMap();
        }

        private void EmployeeBenefitMap()
        {
            CreateMap<EmployeeBenefit, EmployeeBenefitDto>()
                .ReverseMap();
            CreateMap<EmployeeBenefit, UpdateEmployeeBenefitDto>()
                .ReverseMap();
            CreateMap<EmployeeBenefit, ResponseEmployeeBenefitDto>()
                .ReverseMap();
        }

        private void PaySlipMap()
        {
            CreateMap<PaySlip, PaySlipRequestModel>()
                .ReverseMap();
            CreateMap<PaySlip, PaySlipDto>()
                .ReverseMap();
            CreateMap<PaySlip, UpdatePaySlipDto>()
                .ReverseMap();
            CreateMap<MonthlyAllowance, Allowance>()
               .ReverseMap();
        }

        private void ReportMap()
        {
            CreateMap<Report, ReportDto>()
                .ReverseMap();
            CreateMap<Report, UpdateReportDto>()
                .ReverseMap();
            CreateMap<Report, CreateReportDto>()
                .ReverseMap();
        }

        private void OvertimeMap()
        {
            CreateMap<Overtime, OvertimeDto>()
                .ReverseMap();
            CreateMap<Overtime, CreateOvertimeDto>()
                .ReverseMap();
            CreateMap<Overtime, UpdateOvertimeDto>()
                .ReverseMap();
        }

        private void TimesheetMap()
        {
            CreateMap<Timesheet, TimesheetDto>()
                .ReverseMap();
            
        }
    }
}
