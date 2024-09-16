using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class EventService : IEventService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    public EventService(IRepository<Event> eventRepository, IUnitOfWork unitOfWork)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
    }


    public Task<List<Event>> GetEvents()
    {
        return  _eventRepository.GetAll( e=> e.Organizer,e=>e.Registrations);
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
        await _eventRepository.Insert(@event);
        int result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return @event;
    }

    public async Task<Event> UpdateEvent(Event @event)
    {
        ConvertToUTC(@event);
        await _eventRepository.Update(@event);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return @event;
    }

    public async Task<Event> DeleteEvent(Guid? id)
    {
        var @event =await  GetEventById(id);
        
        await _eventRepository.Delete(@event);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return @event;
    }

    public async Task<int> AddEvents(List<Event> events)
    {
        await _eventRepository.InsertMany(events);
        var result = await _unitOfWork.SaveChangesAsync();

        return result;
    }

    private void ConvertToUTC(Event @event)
    {
        @event.StartDate=@event.StartDate.ToUniversalTime();
        @event.EndDate=@event.EndDate.ToUniversalTime();
    }
}