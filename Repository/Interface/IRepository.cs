using System.Linq.Expressions;
using SportEvents.Domain;

namespace Repository.Interface;

public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includeProperties);
        Task<T> Get(Guid? id,params Expression<Func<T, object>>[] includeProperties);
        Task<T> Insert(T entity);
        Task<List<T>> InsertMany(List<T> entities);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }

