using Data_Access_Tier.Entities;

namespace Business_Logic_Tier.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesByDepartmentID(int? departmentId);
        IEnumerable<Employee> Search(string name);
    }
}
