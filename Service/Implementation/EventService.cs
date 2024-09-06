using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class EventService : IEventService
{
    private readonly IRepository<Event> _eventRepository;

    public EventService(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public Task<List<Event>> GetEvents()
    {
        return  _eventRepository.GetAll( e=> e.Organizer);
    }

    public async Task<Event> GetEventById(Guid? id)
    {
        var result = await _eventRepository.Get(id,e=>e.Organizer);
        if (result is null)
        {
            throw new KeyNotFoundException("There is not entity with such id");
        }

        return result;
    }

    public async Task<Event> CreateNewEvent(Event @event)
    {
        ConvertToUTC(@event);
        var result = await _eventRepository.Insert(@event);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Event> UpdateEvent(Event @event)
    {
        ConvertToUTC(@event);
        var result = await _eventRepository.Update(@event);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Event> DeleteEvent(Guid? id)
    {
        var @event =await  GetEventById(id);
        
        var result = await _eventRepository.Delete(@event);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }
    
    private void ConvertToUTC(Event @event)
    {
        @event.StartDate=@event.StartDate.ToUniversalTime();
        @event.EndDate=@event.EndDate.ToUniversalTime();
    }
}