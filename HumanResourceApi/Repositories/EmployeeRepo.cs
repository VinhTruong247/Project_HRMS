using HumanResourceApi.Models;

namespace HumanResourceApi.Repositories
{
    public class EmployeeRepo : BaseRepository.BaseRepository<Employee>
    {
        //public ICollection<Employee> GetEmployeesByDepartment(string departmentId)
        //{
        //    try
        //    {
        //        var employees = GetAll().Where(e => e.DepartmentId == departmentId).ToList();
        //        if (employees == null || employees.Count == 0)
        //        {
        //            throw new ArgumentException();
        //        }
        //        return employees;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
    }
}
