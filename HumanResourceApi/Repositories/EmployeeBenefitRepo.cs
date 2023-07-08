using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceApi.Repositories
{
    public class EmployeeBenefitRepo : BaseRepository.BaseRepository<EmployeeBenefit>
    {
        public decimal GetAllowanceSum(string employeeId)
        {
            decimal allowanceSum = 0;
            var listBenefit = _dbSet.Include(b => b.Allowance).Where(b => b.EmployeeId == employeeId).ToList();
            listBenefit.ForEach(e =>
            {
                allowanceSum += (decimal)e.Allowance.Amount;
            });
            return allowanceSum;
        }
    }
}
