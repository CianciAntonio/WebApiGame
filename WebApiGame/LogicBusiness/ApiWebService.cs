using DataContext.Models;
using DataContext.UnitOfWork;
using Dtos.ModelRequest;
using Dtos.ModelResponse;
using MapsterMapper;
using Microsoft.Extensions.FileSystemGlobbing;

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

        public async Task<Players> GetPlayerAsync(string id)
        {
            Players player = await UnitOfWork.PlayersRepo.GetPlayerAsync(id);

            if (player == null)
                return null;

            UnitOfWork.Dispose();

            return player;
        }

        public async Task RemovePlayer(string id)
        {
            Players dbPlayer = await UnitOfWork.PlayersRepo.GetPlayerAsync(id);

            UnitOfWork.PlayersRepo.RemovePlayer(dbPlayer);
            UnitOfWork.Dispose();
        }

        public async Task AddMatchAsync(MatchRequest matchReq)
        {
            Matches match = Mapper.Map<Matches>(matchReq);

            await UnitOfWork.MatchesRepo.AddMatchAsync(match);
            await UnitOfWork.SaveAsync();
            UnitOfWork.Dispose();
        }

        //public async Task<List<Matches>> GetAllMatchsAsync()
        //{

        //}

        //public async Task<Matches> GetMatchAsync(string id)
        //{

        //}

        //public async Task RemoveMatch(string id)
        //{

        //}

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
