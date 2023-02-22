using DataContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiGame.LogicBusiness;
using Dtos.ModelRequest;

namespace WebApiGame.Controllers
{
    [Route("WebApi")]
    [ApiController]
    public class WebApiGameController : ControllerBase
    {
        private readonly IApiWebService _apiWebService;
        public WebApiGameController(IApiWebService webApiWebService)
        {
            _apiWebService = webApiWebService;
        }

        [HttpPost]
        [Route("AddNewPlayer")]
        public async Task<IActionResult> AddPlayer(PlayerRequest player)
        {
            await _apiWebService.AddPlayerAsync(player);

            return new OkObjectResult(player);
        }

        [HttpGet]
        [Route("GetAllPlayers")]
        public async Task<IActionResult> GetPlayers()
        {
            var allPlayers = await _apiWebService.GetAllPlayersAsync();

            return new OkObjectResult(allPlayers);
        }

        [HttpPost]
        [Route("AddNewMatch")]
        public async Task<IActionResult> AddMatch(MatchRequest match)
        {
            await _apiWebService.AddMatchAsync(match);

            return new OkObjectResult(match);
        }

        [HttpPost]
        [Route("AddNewGame")]
        public async Task<IActionResult> AddGame(GameRequest game)
        {
            await _apiWebService.AddGameAsync(game);

            return new OkObjectResult(game);
        }

        [HttpGet]
        [Route("GetAllGames")]
        public async Task<IActionResult> GetGames()
        {
            var allGames = await _apiWebService.GetAllGamesAsync();

            return new OkObjectResult(allGames);
        }
    }
}
