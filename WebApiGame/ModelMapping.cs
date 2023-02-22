using Dtos.ModelRequest;
using Mapster;
using DataContext.Models;
using Dtos.ModelResponse;

namespace WebApiGame
{
    public class ModelMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Players, PlayerResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.firstName, src => src.firstName)
                .Map(dest => dest.lastName, src => src.lastName)
                .Map(dest => dest.matches, src => src.matches);

            config.ForType<Games, GameResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.matches, src => src.match);

            config.ForType<Matches, MatchesResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.PlayerName, src => $"{src.player.firstName} {src.player.lastName}")
                .Map(dest => dest.GameName, src => src.game.Name)
                .Map(dest => dest.score, src => src.score)
                .Map(dest => dest.date, src => src.date);

            config.ForType<PlayerRequest, Players>();
            config.ForType<GameRequest, Games>();
            config.ForType<MatchRequest, Matches>();

        }
    }
}
