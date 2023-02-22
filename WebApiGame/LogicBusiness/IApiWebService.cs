using DataContext.Models;
using Dtos.ModelRequest;
using Dtos.ModelResponse;

namespace WebApiGame.LogicBusiness
{
    public interface IApiWebService
    {
        Task<List<PlayerResponse>> GetAllPlayersAsync();
        Task<Players> GetPlayerAsync(string id);
        Task AddPlayerAsync(PlayerRequest player);
        Task RemovePlayer(string id);
        //Task<List<MatchResponse>> GetAllMatchsAsync();
        //Task<Matches> GetMatchAsync(string id);
        Task AddMatchAsync(MatchRequest matchReq);
        //Task RemoveMatch(string id);
        Task AddGameAsync(GameRequest gameReq);
        Task<List<GameResponse>> GetAllGamesAsync();
        //Task<Games> GetGameAsync(string id);
        //Task RemoveGame(string id);
    }
}
