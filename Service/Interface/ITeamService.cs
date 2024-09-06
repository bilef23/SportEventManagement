using SportEvents.Domain;

namespace Service.Interface;

public interface ITeamService
{
    public Task<List<Team>> GetTeams();
    public Task<Team> GetTeamById(Guid? id);
    public Task<Team> CreateNewTeam(Team team);
    public Task<Team> UpdateTeam(Team team);
    public Task<Team> DeleteTeam(Guid id);
}