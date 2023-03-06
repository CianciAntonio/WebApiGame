using DataContext;
using DataContext.Models;
using Dtos.ModelRequest;
using Dtos.ModelResponse;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace WebApiGame.LogicBusiness
{
    public interface IApiWebService
    {
        Task<IEnumerable> GetGamesRecord();
        Task<IEnumerable> GetPlayersRecords();
        Task<List<PlayerResponse>> GetAllPlayersAsync();
        Task<PlayerResponse> GetPlayerAsync(string id);
        Task AddPlayerAsync(PlayerRequest player);
        Task<PlayerResponse> UpdatePlayer(PlayerRequest playerReq, string id);
        Task RemovePlayer(string id);
        Task RemovePlayers();
        Task<List<MatchesResponse>> GetAllMatchsAsync();
        //Task<Matches> GetMatchAsync(string id);
        Task AddMatchAsync(MatchRequest matchReq);
        Task RemoveMatch(string id);
        Task AddGameAsync(GameRequest gameReq);
        Task<List<GameResponse>> GetAllGamesAsync();
        //Task<Games> GetGameAsync(string id);
        //Task RemoveGame(string id);
    }
}
