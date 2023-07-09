using HumanResourceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HumanResourceApi.Repositories
{
    public class PaySlipRepo : BaseRepository.BaseRepository<PaySlip>
    {
        public decimal GetTaxIncome(string employeeId, DateTime targetDate)
        {
            var employee = _context.Employees.Include(e => e.Job).Where(e => e.EmployeeId == employeeId).FirstOrDefault();
            var contract = _context.EmployeeContracts.Where(e => e.EmployeeId == employeeId).FirstOrDefault() ?? throw new Exception("contract not found");
            var ot = _context.Overtimes.Where(o => o.EmployeeId == employeeId).ToList();
            var filteredOt = ot.Where(o => o.Day.Year == targetDate.Year && o.Day.Month == targetDate.Year).ToList();
            decimal otHour = 0;
            filteredOt.ForEach(fo =>
            {
                otHour += (decimal)fo.OvertimeHours.Value.TotalHours;
            });
            
            decimal taxIncome = 0;
            decimal baseSalary = (decimal)contract.BaseSalary;
            decimal bonus = (decimal)employee.Job.Bonus;
            decimal overtime = otHour;


            return taxIncome = baseSalary + bonus + overtime;
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
