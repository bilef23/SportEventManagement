using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class OrganizerService : IOrganizerService
{
    private readonly IRepository<Organizer> _organizerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrganizerService(IRepository<Organizer> organizerRepository, IUnitOfWork unitOfWork)
    {
        _organizerRepository = organizerRepository;
        _unitOfWork = unitOfWork;
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
        await _organizerRepository.Insert(organizer);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <=0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return organizer;
    }

    public async Task<Organizer> UpdateOrganizer(Organizer organizer)
    {
        await _organizerRepository.Update(organizer);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0 )
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return organizer;
    }

    public async Task<Organizer> DeleteOrganizer(Guid id)
    {
        var organizer =await  GetOrganizerById(id);
        
        await _organizerRepository.Delete(organizer);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return organizer;
    }
}