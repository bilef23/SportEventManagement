using SportEvents.Domain;

namespace Service.Interface;

public interface IOrganizerService
{
    public Task<List<Organizer>> GetOrganizers();
    public Task<Organizer> GetOrganizerById(Guid? id);
    public Task<Organizer> CreateNewOrganizer(Organizer organizer);
    public Task<Organizer> UpdateOrganizer(Organizer organizer);
    public Task<Organizer> DeleteOrganizer(Guid id);
}