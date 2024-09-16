using SportEvents.Domain;
namespace Service.Interface;

public interface IEventService
{
    public Task<List<Event>> GetEvents();
    public Task<Event> GetEventById(Guid? id);
    public Task<Event> CreateNewEvent(Event Event);
    public Task<Event> UpdateEvent(Event Event);
    public Task<Event> DeleteEvent(Guid? id);
    public Task<int> AddEvents(List<Event> events);
}