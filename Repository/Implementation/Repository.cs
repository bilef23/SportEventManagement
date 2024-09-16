using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SportEvents.Domain;
using Repository.Interface;


namespace Repository.Implementation;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private DbSet<T> _entities;
    //string errorMessage = string.Empty;

    public Repository(ApplicationDbContext context)
    {
        this._context = context;
        _entities = context.Set<T>();
    }
    public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _entities;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }

    public async Task<T> Get(Guid? id, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _entities;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<T> Insert(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        await _entities.AddAsync(entity);
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _entities.Update(entity);
        return entity;
    }

    public async Task<T> Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _entities.Remove(entity);
        return entity;
    }

    public async Task<List<T>> InsertMany(List<T> entities)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        _entities.AddRange(entities);
        return entities;
    }

}
