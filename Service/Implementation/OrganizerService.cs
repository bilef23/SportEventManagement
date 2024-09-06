using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class OrganizerService : IOrganizerService
{
    private readonly IRepository<Organizer> _organizerRepository;

    public OrganizerService(IRepository<Organizer> organizerRepository)
    {
        _organizerRepository = organizerRepository;
    }

    public async Task<List<Organizer>> GetOrganizers()
    {
        return await _organizerRepository.GetAll();
    }

    public async Task<Organizer> GetOrganizerById(Guid? id)
    {
        var result = await _organizerRepository.Get(id);
        if (result is null)
        {
            throw new KeyNotFoundException("There is not entity with such id");
        }

        return result;
    }

    public async Task<Organizer> CreateNewOrganizer(Organizer organizer)
    {
        var result = await _organizerRepository.Insert(organizer);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Organizer> UpdateOrganizer(Organizer organizer)
    {
        var result = await _organizerRepository.Update(organizer);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Organizer> DeleteOrganizer(Guid id)
    {
        var organizer =await  GetOrganizerById(id);
        
        var result = await _organizerRepository.Delete(organizer);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }
}