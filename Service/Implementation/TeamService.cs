using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class TeamService : ITeamService
{
    private readonly IRepository<Team> _teamRepository;
    private readonly IUnitOfWork _unitOfWork;
    public TeamService(IRepository<Team> teamRepository, IUnitOfWork unitOfWork)
    {
        _teamRepository = teamRepository;
        _unitOfWork = unitOfWork;
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
        await _teamRepository.Insert(team);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return team;
    }

    public async Task<Team> UpdateTeam(Team team)
    {
        await _teamRepository.Update(team);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return team;
    }

    public async Task<Team> DeleteTeam(Guid id)
    {
        var team =await  GetTeamById(id);
        
        await _teamRepository.Delete(team);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result <= 0)
        {
            throw new OperationCanceledException("Action can not be executed");
        }

        return team;
    }
}