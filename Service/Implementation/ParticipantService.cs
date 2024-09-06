using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class ParticipantService : IParticipantService
{
    private readonly IRepository<Participant> _participantRepository;

    public ParticipantService(IRepository<Participant> participantRepository)
    {
        _participantRepository = participantRepository;
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
        var result = await _participantRepository.Insert(participant);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Participant> UpdateParticipant(Participant participant)
    {
        var result = await _participantRepository.Update(participant);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Participant> DeleteParticipant(Guid id)
    {
        var participant =await  GetParticipantById(id);
        
        var result = await _participantRepository.Delete(participant);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }
}