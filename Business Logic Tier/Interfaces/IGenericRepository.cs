using Data_Access_Tier.Entities;

namespace Business_Logic_Tier.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        T GetById(int? Id);
        IEnumerable<T> GetAll();
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
