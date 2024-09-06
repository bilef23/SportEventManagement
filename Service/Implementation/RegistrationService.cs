using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class RegistrationService : IRegistrationService
{
    private readonly IRepository<Registration> _registrationRepository;

    public RegistrationService(IRepository<Registration> registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<List<Registration>> GetRegistrations()
    {
        return await _registrationRepository.GetAll();
    }

    public async Task<Registration> GetRegistrationById(Guid? id)
    {
        var result = await _registrationRepository.Get(id);
        if (result is null)
        {
            throw new KeyNotFoundException("There is not entity with such id");
        }

        return result;
    }

    public async Task<Registration> CreateNewRegistration(Registration registration)
    {
        var result = await _registrationRepository.Insert(registration);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Registration> UpdateRegistration(Registration registration)
    {
        var result = await _registrationRepository.Update(registration);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
        
    }

    public async Task<Registration> DeleteRegistration(Guid id)
    {
        var registration =await  GetRegistrationById(id);
        
        var result = await _registrationRepository.Delete(registration);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }
}