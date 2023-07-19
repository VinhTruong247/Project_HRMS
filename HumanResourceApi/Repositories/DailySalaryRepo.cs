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
    }
}
