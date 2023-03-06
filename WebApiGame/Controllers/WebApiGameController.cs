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

        [HttpGet]
        [Route("GetGamesRecord")]
        [Tags("Games")]
        public async Task<IActionResult> GetGamesRecord()
        {
            var allGames = await _apiWebService.GetGamesRecord();

            return new OkObjectResult(allGames);
        }

        [HttpGet]
        [Route("GetPlayersRecord")]
        [Tags("Players")]
        public async Task<IActionResult> GetPlayersRecord()
        {
            var allPlayers = await _apiWebService.GetPlayersRecords();

            return new OkObjectResult(allPlayers);
        }

        [HttpPost]
        [Route("AddNewPlayer")]
        [Tags("Players")]
        public async Task<IActionResult> AddPlayer(PlayerRequest player)
        {
            await _apiWebService.AddPlayerAsync(player);

            return new OkObjectResult(player);
        }

        [HttpGet]
        [Route("GetAllPlayers")]
        [Tags("Players")]
        public async Task<IActionResult> GetPlayers()
        {
            var allPlayers = await _apiWebService.GetAllPlayersAsync();

            return new OkObjectResult(allPlayers);
        }

        [HttpGet]
        [Route("GetPlayer")]
        [Tags("Players")]
        public async Task<IActionResult> GetPlayer(string id)
        {
            var player = await _apiWebService.GetPlayerAsync(id);

            return new OkObjectResult(player);
        }

        [HttpPut]
        [Route("UpdatePlayer")]
        [Tags("Players")]
        public async Task<IActionResult> UpdatePlayer(PlayerRequest playerReq, string id)
        {
            await _apiWebService.UpdatePlayer(playerReq, id);

            return new OkObjectResult("Player Updated");
        }

        [HttpDelete]
        [Route("RemovePlayer")]
        [Tags("Players")]
        public async Task<IActionResult> RemovePlayer(string id)
        {
            await _apiWebService.RemovePlayer(id);

            return new OkObjectResult("Player Removed");
        }

        [HttpDelete]
        [Route("RemoveAllPlayers")]
        [Tags("Players")]
        public async Task<IActionResult> RemovePlayers()
        {
            await _apiWebService.RemovePlayers();

            return new OkObjectResult("PLayers Removed");
        }

        [HttpPost]
        [Route("AddNewMatch")]
        [Tags("Matches")]
        public async Task<IActionResult> AddMatch(MatchRequest match)
        {
            await _apiWebService.AddMatchAsync(match);

            return new OkObjectResult(match);
        }

        [HttpGet]
        [Route("GetAllMatches")]
        [Tags("Matches")]
        public async Task<IActionResult> GetMatches()
        {
            var allMatches = await _apiWebService.GetAllMatchsAsync();

            return new OkObjectResult(allMatches);
        }

        [HttpDelete]
        [Route("RemoveMatch")]
        [Tags("Matches")]
        public async Task<IActionResult> RemoveMatch(string id)
        {
            await _apiWebService.RemoveMatch(id);

            return new OkObjectResult("Match Removed");
        }

        [HttpPost]
        [Route("AddNewGame")]
        [Tags("Games")]
        public async Task<IActionResult> AddGame(GameRequest game)
        {
            await _apiWebService.AddGameAsync(game);

            return new OkObjectResult(game);
        }

        [HttpGet]
        [Route("GetAllGames")]
        [Tags("Games")]
        public async Task<IActionResult> GetGames()
        {
            var allGames = await _apiWebService.GetAllGamesAsync();

            return new OkObjectResult(allGames);
        }
    }
}
