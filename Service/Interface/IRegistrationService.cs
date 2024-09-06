using SportEvents.Domain;

namespace Service.Interface;

public interface IRegistrationService
{
    public Task<List<Registration>> GetRegistrations();
    public Task<Registration> GetRegistrationById(Guid? id);
    public Task<Registration> CreateNewRegistration(Registration registration);
    public Task<Registration> UpdateRegistration(Registration registration);
    public Task<Registration> DeleteRegistration(Guid id);
}