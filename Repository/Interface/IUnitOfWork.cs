namespace Repository.Interface;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}