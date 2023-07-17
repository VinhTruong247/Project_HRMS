using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceApi.Repositories
{
    public class EmployeeBenefitRepo : BaseRepository.BaseRepository<EmployeeBenefit>
    {
        public decimal GetAllowanceSum(string employeeId, decimal actualHours)
        {
            decimal allowanceSum = 0;
            decimal actualDate = actualHours / 8;
            actualDate = Math.Round(actualDate, 2);
            var listBenefit = _dbSet.Include(b => b.Allowance).Where(b => b.EmployeeId == employeeId).ToList();
            listBenefit.ForEach(e =>
            {
                if (e.Allowance.AllowanceType.ToLower().Equals("daily"))
                {
                    allowanceSum += actualDate * (e.Allowance.Amount);
                }
                else
                {
                    allowanceSum += e.Allowance.Amount;
                }
            });
            return allowanceSum;
        }

    }
}
