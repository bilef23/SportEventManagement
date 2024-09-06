using SportEvents.Domain;
namespace Service.Interface;

public interface IEventService
{
    public List<Event> GetEvents();
    public Event GetEventById(Guid? id);
    public Event CreateNewEvent(Event Event);
    public Event UpdateEvent(Event Event);
    public Event DeleteEvent(Guid id);
}