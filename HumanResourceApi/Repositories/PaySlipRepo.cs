using HumanResourceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HumanResourceApi.Repositories
{
    public class PaySlipRepo : BaseRepository.BaseRepository<PaySlip>
    {
        public decimal GetTotalSalary(decimal baseSalary, decimal allowanceSum, decimal tax, decimal otSalary)
        {
            decimal salary = 0;
            return salary = baseSalary + allowanceSum + (otSalary * (decimal)1.5) - tax;
        }
        public decimal GetTaxIncome(decimal baseSalary, decimal overtime, int depends)
        {
            decimal taxIncome = baseSalary + overtime - 11000000 - depends * 4400000;
            if (taxIncome < 0)
            {
                return 0;
            }
            return taxIncome;
        }

        public decimal GetTax(decimal taxIncome)
        {
            decimal tax = 0;
            if(taxIncome <= 5000000)
            {
                return tax = taxIncome * (decimal)0.05;
            }
            if (taxIncome <= 10000000)
            {
                return tax = ((taxIncome - 5000000) * (decimal)0.1) + 250000;
            }
            if (taxIncome <= 18000000)
            {
                return tax = ((taxIncome - 10000000) * (decimal)0.15) + 750000;
            }
            if (taxIncome <= 32000000)
            {
                return tax = ((taxIncome - 18000000) * (decimal)0.2) + 1950000;
            }
            if (taxIncome <= 52000000)
            {
                return tax = ((taxIncome - 32000000) * (decimal)0.25) + 4750000;
            }
            if (taxIncome <= 80000000)
            {
                return tax = ((taxIncome - 52000000) * (decimal)0.3) + 9750000;
            }
            return tax = ((taxIncome - 80000000) * (decimal)0.35) + 18150000;
        }
    }
}
