using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Web.Controllers;

public class GameController : Controller
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var games = await _gameService.GetAllGames();
        
        return View(games);
    }
}