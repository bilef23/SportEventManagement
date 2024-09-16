using Repository.Interface;
using Service.Interface;
using SportEvents.Domain;

namespace Service.Implementation;

public class GameService : IGameService
{
    private readonly IRepository<GameFromPartnerStore> _gameRepository;

    public GameService(IRepository<GameFromPartnerStore> gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<List<GameFromPartnerStore>> GetAllGames()
    {
        return await _gameRepository.GetAll();
    }
}