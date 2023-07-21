using HumanResourceApi.DTO.DailySalary;
using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceApi.Repositories
{
    public class EmployeeBenefitRepo : BaseRepository.BaseRepository<EmployeeBenefit>
    {
        public decimal GetAllowanceSum(string employeeId, decimal dailyAllowanceSum)
        {
            decimal allowanceSum = dailyAllowanceSum;
            var listBenefit = _dbSet.Include(b => b.Allowance).Where(b => b.EmployeeId == employeeId).ToList();
            listBenefit.ForEach(e =>
            {
                if (e.Allowance.AllowanceType.ToLower().Equals("monthly"))
                {
                    allowanceSum += e.Allowance.Amount;
                }
            });
            return allowanceSum;
        }

        public decimal GetDailyAllowance(string employeeId)
        {
            decimal allowanceSum = 0;
            var listBenefit = _dbSet.Include(b => b.Allowance).Where(b => b.EmployeeId == employeeId).ToList();
            listBenefit.ForEach(e =>
            {
                if (e.Allowance.AllowanceType.ToLower().Equals("daily"))
                {
                    allowanceSum += e.Allowance.Amount;
                }
            });
            return allowanceSum;
        }
        public decimal GetMonthlyAllowance(string employeeId)
        {
            decimal allowanceSum = 0;
            var listBenefit = _dbSet.Include(b => b.Allowance).Where(b => b.EmployeeId == employeeId).ToList();
            listBenefit.ForEach(e =>
            {
                if (e.Allowance.AllowanceType.ToLower().Equals("monthly"))
                {
                    allowanceSum += e.Allowance.Amount;
                }
            });
            return allowanceSum;
        }

        public List<Allowance> GetDailyAllowanceDetail(string employeeId)
        {
            var listBenefit = _dbSet.Include(b => b.Allowance).Where(b => b.EmployeeId == employeeId).ToList();
            var listAllowance = new List<Allowance>();
            listBenefit.ForEach(e =>
            {
                if (e.Allowance.AllowanceType.ToLower().Equals("daily"))
                {
                    listAllowance.Add(e.Allowance);
                }
            });
            return listAllowance;
        }
        public List<Allowance> GetMonthlyAllowanceDetail(string employeeId)
        {
            var listBenefit = _dbSet.Include(b => b.Allowance).Where(b => b.EmployeeId == employeeId).ToList();
            var listAllowance = new List<Allowance>();
            listBenefit.ForEach(e =>
            {
                if (e.Allowance.AllowanceType.ToLower().Equals("monthly"))
                {
                    listAllowance.Add(e.Allowance);
                }
            });
            return listAllowance;
        }

    }
}
