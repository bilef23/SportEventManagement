using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementation;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private DbSet<SportEventsAppUser> _entities;

    public UserRepository(ApplicationDbContext context)
    {
        this._context = context;
        _entities = context.Set<SportEventsAppUser>();
    }
    public IEnumerable<SportEventsAppUser> GetAll()
    {
        return _entities.AsEnumerable();
    }

    public SportEventsAppUser Get(string id)
    {
        return _entities
            .Include(z=>z.Orders)
            .Include("Orders.TicketsInOrder")
            .Include("Orders.TicketsInOrder.Ticket")
            .Include("Orders.TicketsInOrder.Ticket.Event")
            .Include(z => z.ShoppingCart)
            .Include("ShoppingCart.Tickets")
            .Include("ShoppingCart.Tickets.Event")
            .SingleOrDefault(s => s.Id == id);
    }
    public void Insert(SportEventsAppUser entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        _entities.Add(entity);
        _context.SaveChanges();
    }

    public void Update(SportEventsAppUser entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        _entities.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(SportEventsAppUser entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        _entities.Remove(entity);
        _context.SaveChanges();
    }
}