using Business_Logic_Tier.Interfaces;
using Data_Access_Tier.Context;
using Data_Access_Tier.Entities;

namespace Business_Logic_Tier.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MVCAppDbContext _context;

        public GenericRepository(MVCAppDbContext context)
        {
            _context = context;
        }

        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
            => _context.Set<T>().ToList();

        public T GetById(int? Id)
            => _context.Set<T>().Find(Id);

        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
    }
}
