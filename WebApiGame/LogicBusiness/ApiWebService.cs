using DataContext;
using DataContext.Models;
using DataContext.UnitOfWork;
using Dtos.ModelRequest;
using Dtos.ModelResponse;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace WebApiGame.LogicBusiness
{
    public class ApiWebService : IApiWebService
    {
        public IUnitOfWork UnitOfWork;
        public IMapper Mapper;

        public ApiWebService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<IEnumerable> GetGamesRecord()
        {
            var result = await UnitOfWork.GamesRepo.GetGamesRecord();

            return result;
        }

        public async Task<IEnumerable> GetPlayersRecords()
        {
            var result = await UnitOfWork.PlayersRepo.GetPlayersRecords();

            return result;
        }

        public async Task AddPlayerAsync(PlayerRequest playerReq)
        {
            Players player = Mapper.Map<Players>(playerReq);

            await UnitOfWork.PlayersRepo.AddPlayerAsync(player);
            await UnitOfWork.SaveAsync();
            UnitOfWork.Dispose();
        }

        public async Task<List<PlayerResponse>> GetAllPlayersAsync()
        {
            List<PlayerResponse> playersDto = new List<PlayerResponse>();
            
            List<Players> players = await UnitOfWork.PlayersRepo.GetAllPlayersAsync();

            if (players == null)
                return null;

            foreach (var player in players)
            {
                PlayerResponse playerDto = Mapper.Map<PlayerResponse>(player);
                playersDto.Add(playerDto);
            }

            UnitOfWork.Dispose();

            return playersDto;
        }

        public async Task<PlayerResponse> GetPlayerAsync(string id)
        {
            Players player = await UnitOfWork.PlayersRepo.GetPlayerAsync(id);

            if (player == null)
                return null;

            PlayerResponse playerDto = Mapper.Map<PlayerResponse>(player);

            UnitOfWork.Dispose();

            return playerDto;
        }

        public async Task<PlayerResponse> UpdatePlayer(PlayerRequest playerReq, string id)
        {
            Players dbPlayer = await UnitOfWork.PlayersRepo.GetPlayerAsync(id);

            if(dbPlayer == null)
                return null;

            dbPlayer.firstName = playerReq.firstName;
            dbPlayer.lastName = playerReq.lastName;

            UnitOfWork.PlayersRepo.UpdatePlayerAsync(dbPlayer);
            await UnitOfWork.SaveAsync();
            UnitOfWork.Dispose();

            var playerDto = Mapper.Map<PlayerResponse>(dbPlayer);

            return playerDto;
        }

        public async Task RemovePlayer(string id)
        {
            Players dbPlayer = await UnitOfWork.PlayersRepo.GetPlayerAsync(id);

            UnitOfWork.PlayersRepo.RemovePlayer(dbPlayer);
            await UnitOfWork.SaveAsync();
            UnitOfWork.Dispose();
        }

        public async Task RemovePlayers()
        {
            List<Players> players = await UnitOfWork.PlayersRepo.GetAllPlayersAsync();

            UnitOfWork.PlayersRepo.RemoveAllPlayers(players);
            await UnitOfWork.SaveAsync();
            UnitOfWork.Dispose();
        }

        public async Task AddMatchAsync(MatchRequest matchReq)
        {
            Matches match = Mapper.Map<Matches>(matchReq);

            await UnitOfWork.MatchesRepo.AddMatchAsync(match);
            await UnitOfWork.SaveAsync();
            UnitOfWork.Dispose();
        }

        public async Task<List<MatchesResponse>> GetAllMatchsAsync()
        {
            List<MatchesResponse> matchesDto = new List<MatchesResponse>();

            List<Matches> matches = await UnitOfWork.MatchesRepo.GetAllMatchesAsync();

            if (matches== null)
                return null;

            foreach (var match in matches)
            {
                MatchesResponse matchDto = Mapper.Map<MatchesResponse>(match);
                matchesDto.Add(matchDto);
            }

            UnitOfWork.Dispose();

            return matchesDto;
        }

        //public async Task<Matches> GetMatchAsync(string id)
        //{

        //}

        public async Task RemoveMatch(string id)
        {
            Matches match = await UnitOfWork.MatchesRepo.GetMatchAsync(id);

            UnitOfWork.MatchesRepo.RemoveMatch(match);
            await UnitOfWork.SaveAsync();
            UnitOfWork.Dispose();
        }

        public async Task AddGameAsync(GameRequest gameReq)
        {
            Games game = Mapper.Map<Games>(gameReq);

            await UnitOfWork.GamesRepo.AddGameAsync(game);
            await UnitOfWork.SaveAsync();
            UnitOfWork.Dispose();
        }

        public async Task<List<GameResponse>> GetAllGamesAsync()
        {
            List<GameResponse> gamesResponse= new List<GameResponse>();

            List<Games> games = await UnitOfWork.GamesRepo.GetAllGamesAsync();

            if (games == null)
                return null;

            foreach (var game in games)
            {
                GameResponse gameDto = Mapper.Map<GameResponse>(game);
                gamesResponse.Add(gameDto);
            }

            UnitOfWork.Dispose();

            return gamesResponse;
        }

        //public async Task<Games> GetGameAsync(string id)
        //{

        //}

        //public async Task RemoveGame(string id)
        //{

        //}
    }
}
