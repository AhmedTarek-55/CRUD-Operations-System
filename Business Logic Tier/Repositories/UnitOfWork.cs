using Business_Logic_Tier.Interfaces;

namespace Business_Logic_Tier.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            EmployeeRepository = employeeRepository;
            DepartmentRepository = departmentRepository;
        }

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
    }
}
