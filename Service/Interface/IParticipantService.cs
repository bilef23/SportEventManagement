using SportEvents.Domain;

namespace Service.Interface;

public interface IParticipantService
{
    public Task<List<Participant>> GetParticipants();
    public Task<Participant> GetParticipantById(Guid? id);
    public Task<Participant> CreateNewParticipant(Participant participant);
    public Task<Participant> UpdateParticipant(Participant participant);
    public Task<Participant> DeleteParticipant(Guid id);
}