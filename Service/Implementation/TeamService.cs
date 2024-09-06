using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class TeamService : ITeamService
{
    private readonly IRepository<Team> _teamRepository;

    public TeamService(IRepository<Team> teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public Task<List<Team>> GetTeams()
    {
        return _teamRepository.GetAll();
    }

    public async Task<Team> GetTeamById(Guid? id)
    {
        var result = await _teamRepository.Get(id);
        if (result is null)
        {
            throw new KeyNotFoundException("There is not entity with such id");
        }

        return result;
    }

    public async Task<Team> CreateNewTeam(Team team)
    {
        var result = await _teamRepository.Insert(team);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Team> UpdateTeam(Team team)
    {
        var result = await _teamRepository.Update(team);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }

    public async Task<Team> DeleteTeam(Guid id)
    {
        var ticket =await  GetTeamById(id);
        
        var result = await _teamRepository.Delete(ticket);
        if (result is null)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return result;
    }
}