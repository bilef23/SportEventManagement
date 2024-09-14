using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class ParticipantService : IParticipantService
{
    private readonly IRepository<Participant> _participantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantService(IRepository<Participant> participantRepository, IUnitOfWork unitOfWork)
    {
        _participantRepository = participantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Participant>> GetParticipants()
    {
        return await _participantRepository.GetAll();
    }

    public async Task<Participant> GetParticipantById(Guid? id)
    {
        var result = await _participantRepository.Get(id);
        if (result is null)
        {
            throw new KeyNotFoundException("There is not entity with such id");
        }

        return result;
    }

    public async Task<Participant> CreateNewParticipant(Participant participant)
    {
        await _participantRepository.Insert(participant);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return participant;
    }

    public async Task<Participant> UpdateParticipant(Participant participant)
    {
        await _participantRepository.Update(participant);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return participant;
    }

    public async Task<Participant> DeleteParticipant(Guid id)
    {
        var participant =await  GetParticipantById(id);
        
        await _participantRepository.Delete(participant);
        var result = await _unitOfWork.SaveChangesAsync();
        
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return participant;
    }
}