using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class RegistrationService : IRegistrationService
{
    private readonly IRepository<Registration> _registrationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrationService(IRepository<Registration> registrationRepository, IUnitOfWork unitOfWork)
    {
        _registrationRepository = registrationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Registration>> GetRegistrations()
    {
        return await _registrationRepository.GetAll(e=>e.Event,e=>e.Participant);
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
        await _registrationRepository.Insert(registration);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return registration;
    }

    public async Task<Registration> UpdateRegistration(Registration registration)
    {
        await _registrationRepository.Update(registration);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return registration;
        
    }

    public async Task<Registration> DeleteRegistration(Guid id)
    {
        var registration =await  GetRegistrationById(id);
        
        await _registrationRepository.Delete(registration);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return registration;
    }
}