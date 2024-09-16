using SportEvents.Domain;

namespace Service.Interface;

public interface IGameService
{
    Task<List<GameFromPartnerStore>> GetAllGames();
}