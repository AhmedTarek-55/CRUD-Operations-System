using Business_Logic_Tier.Interfaces;
using Data_Access_Tier.Context;
using Data_Access_Tier.Entities;

namespace Business_Logic_Tier.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly MVCAppDbContext _context;

        public DepartmentRepository(MVCAppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
