using Domain.Identity;

namespace Repository.Interface;

public interface IUserRepository
{
    IEnumerable<SportEventsAppUser> GetAll();
    SportEventsAppUser Get(string? id);
    void Insert(SportEventsAppUser entity);
    void Update(SportEventsAppUser entity);
    void Delete(SportEventsAppUser entity);
}