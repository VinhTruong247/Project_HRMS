using HumanResourceApi.Models;

namespace HumanResourceApi.Repositories
{
    public class DailySalaryRepo : BaseRepository.BaseRepository<DailySalary>
    {
        public decimal GetDailySalary(decimal totalHours, decimal salaryPerHour, decimal otSalary, decimal dailyAllowance)
        {
            decimal dailySalary = 0;
            totalHours = Math.Round(totalHours, 2);
            decimal baseSalary = totalHours * salaryPerHour;
            dailySalary += otSalary + baseSalary + dailyAllowance;
            dailySalary = Math.Round(dailySalary, 2);
            return dailySalary;
        }
        public decimal GetAllowance(string employeeId, DateTime date, decimal dailyAllowance)
        {
            var dailySalaryList = GetAll().Where(d => d.EmployeeId == employeeId
            && d.Date.Year == date.Year && d.Date.Month == date.Month).ToList();
            if (dailySalaryList is null)
            {
                return 0;
            }
            decimal allowanceSum = 0;
            dailySalaryList.ForEach(d =>
            {
                allowanceSum += dailyAllowance;
            });
            return allowanceSum;
        }
        public decimal GetOtSalary(string employeeId, DateTime date)
        {
            var dailySalaryList = GetAll().Where(d => d.EmployeeId == employeeId
            && d.Date.Year == date.Year && d.Date.Month == date.Month).ToList();
            if (dailySalaryList is null)
            {
                return 0;
            }
            decimal otSalary = 0;
            dailySalaryList.ForEach(d =>
            {
                otSalary += Math.Round((decimal)d.OtSalary, 2);
            });
            return otSalary;
        }
        public decimal GetBaseSalary(string employeeId, DateTime date)
        {
            var dailySalaryList = GetAll().Where(d => d.EmployeeId == employeeId
            && d.Date.Year == date.Year && d.Date.Month == date.Month).ToList();
            if (dailySalaryList is null)
            {
                return 0;
            }
            decimal baseSalary = 0;
            dailySalaryList.ForEach(d =>
            {
                baseSalary += Math.Round((decimal)d.TotalSalary, 2);
            });
            return baseSalary;
        }
        public decimal GetMonthlyHour(string employeeId, DateTime date)
        {
            var dailySalaryList = GetAll().Where(d => d.EmployeeId == employeeId 
            && d.Date.Year == date.Year && d.Date.Month == date.Month).ToList();
            if(dailySalaryList is null)
            {
                return 0;
            }
            decimal monthlyHour = 0;
            dailySalaryList.ForEach(d =>
            {
                monthlyHour += Math.Round((decimal)d.TotalHours.TotalHours, 2);
            });
            return monthlyHour;
        }

        public decimal GetMonthlyOtHour(string employeeId, DateTime date)
        {
            var dailySalaryList = GetAll().Where(d => d.EmployeeId == employeeId
            && d.Date.Year == date.Year && d.Date.Month == date.Month).ToList();
            if (dailySalaryList is null)
            {
                return 0;
            }
            decimal monthlyOtHour = 0;
            dailySalaryList.ForEach(d =>
            {
                monthlyOtHour += Math.Round((decimal)d.OtHours.TotalHours, 2);
            });
            return monthlyOtHour;
        }
    }
}
