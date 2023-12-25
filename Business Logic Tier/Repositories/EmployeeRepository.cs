using Business_Logic_Tier.Interfaces;
using Data_Access_Tier.Context;
using Data_Access_Tier.Entities;

namespace Business_Logic_Tier.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppDbContext _context;

        public EmployeeRepository(MVCAppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployeesByDepartmentID(int? departmentId)
            => _context.Employees.Where(emp => emp.DepartmentId == departmentId);

        public IEnumerable<Employee> Search(string name)
            => _context.Employees.Where(emp => emp.Name.Trim().ToLower().Contains(name.Trim().ToLower()));
    }
}