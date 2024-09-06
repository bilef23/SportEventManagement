using SportEvents.Domain;

namespace Repository.Interface;

public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid? id);
        T Insert(T entity);
        List<T> InsertMany(List<T> entities);
        T Update(T entity);
        T Delete(T entity);
    }

