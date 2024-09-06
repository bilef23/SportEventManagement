using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class EventService : Interface.IEventService
{
    private readonly IRepository<Event> _eventRepository;

    public EventService(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public List<Event> GetEvents()
    {
        return _eventRepository.GetAll().ToList();
    }

    public Event GetEventById(Guid? id)
    {
        throw new NotImplementedException();
    }

    public Event CreateNewEvent(Event Event)
    {
        throw new NotImplementedException();
    }

    public Event UpdateEvent(Event Event)
    {
        throw new NotImplementedException();
    }

    public Event DeleteEvent(Guid id)
    {
        throw new NotImplementedException();
    }
}