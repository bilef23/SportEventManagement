using Repository.Interface;

namespace Repository.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;


    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}