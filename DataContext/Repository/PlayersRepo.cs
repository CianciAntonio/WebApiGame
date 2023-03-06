using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace DataContext.Repository
{
    public class PlayersRepo : Repository<Players>, IPlayersRepo
    {
        private AppDbContext _appDbContext;

        public PlayersRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<List<Players>> GetAllPlayersAsync()
        {
            var players = await _appDbContext.Players
                .AsNoTracking()
                .Include(y => y.matches)
                .ThenInclude(y => y.game)
                .ToListAsync();

            return players;
        }

        //Scrivere una query che restituisca l'elenco dei nomi di TUTTI i giocatori, e per ciascuno il numero di giochi
        //in cui attualmente detiene il record. I record a parimerito vanno conteggiati per tutti i giocatori detentori.
        public async Task<IEnumerable> GetPlayersRecords()
        {
            var recordPlayers = from p in _appDbContext.Players
                                join m in _appDbContext.Matches
                                on p.Id equals m.idPlayer into matchesGroup
                                from mg in matchesGroup
                                where mg.score == (from m in _appDbContext.Matches
                                                   where m.idGame == mg.idGame
                                                   select m.score).Max()
                                group matchesGroup by new { p.Id, p.firstName, p.lastName } into g
                                select new
                                {
                                    g.Key.Id,
                                    g.Key.firstName,
                                    g.Key.lastName,
                                    Record_Count = g.Count()
                                };

            var otherPlayers = from p in _appDbContext.Players
                               join rp in recordPlayers
                               on p.Id equals rp.Id into allPlayers
                               from ap in allPlayers.DefaultIfEmpty()
                               where ap.Id != p.Id
                               select new
                               {
                                   p.Id,
                                   p.firstName,
                                   p.lastName,
                                   Record_Count = 0
                               };

            var filterResult = from r in recordPlayers.Concat(otherPlayers)
                               orderby r.Record_Count descending
                               select new
                               {
                                   r.firstName,
                                   r.lastName,
                                   r.Record_Count
                               };

            #region Versione Bing
            //var maxScores = from match in _appDbContext.Matches
            //                group match by match.idGame into g
            //                select new { GameId = g.Key, MaxScore = g.Max(m => m.score) };

            //var filterResult = from player in _appDbContext.Players
            //                   join match in _appDbContext.Matches on player.Id equals match.idPlayer into playerMatches
            //                   from playerMatch in playerMatches.DefaultIfEmpty()
            //                   join maxScore in maxScores on playerMatch.idGame equals maxScore.GameId into maxScoresPerGame
            //                   from maxScorePerGame in maxScoresPerGame.DefaultIfEmpty()
            //                   group new
            //                   {
            //                       player.firstName,
            //                       player.lastName,
            //                       Score = (playerMatch == null ? 0 : (playerMatch.score == maxScorePerGame.MaxScore ? 1 : 0))
            //                   }
            //                   by new { player.firstName, player.lastName } into g
            //                   select new
            //                   {
            //                       Name = g.Key.firstName,
            //                       g.Key.lastName,
            //                       RecordCount = g.Sum(x => x.Score)
            //                   };



            //var maxScores = from match in _appDbContext.Matches
            //                group match.score by match.idGame into g
            //                select new
            //                {
            //                    GameId = g.Key,
            //                    MaxScore = g.Max()
            //                };

            //var recordPlayers = from player in _appDbContext.Players

            //                    join match in _appDbContext.Matches
            //                    on player.Id equals match.idPlayer into playerMatches
            //                    from playerMatch in playerMatches.DefaultIfEmpty()

            //                    join game in _appDbContext.Games
            //                    on playerMatch.idGame equals game.Id into playerGames
            //                    from playerGame in playerGames.DefaultIfEmpty()

            //                    join maxScore in maxScores
            //                    on playerMatch.idGame equals maxScore.GameId into maxScoresPerGame
            //                    from maxScorePerGame in maxScoresPerGame.DefaultIfEmpty()

            //                    group new 
            //                    { 
            //                        player.firstName, player.lastName, 
            //                        Score = (playerMatch == null ? 0 : 
            //                        (playerMatch.score == maxScorePerGame.MaxScore ? 1 : 0)) 
            //                    } 
            //                    by new { player.firstName, player.lastName } into g
            //                    select new
            //                    {
            //                        Name = g.Key.firstName + " " + g.Key.lastName,
            //                        RecordCount = g.Sum(x => x.Score)
            //                    };
            #endregion

            return filterResult;
        }

        public async Task<Players> GetPlayerAsync(string id)
        {
            Players? player = await _appDbContext.Players
                .AsNoTracking()
                .Include(y => y.matches)
                .ThenInclude(y => y.game)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return player;
        }

        public async Task AddPlayerAsync(Players player)
        {
            await _appDbContext.Players.AddAsync(player);
        }

        public void UpdatePlayerAsync(Players player)
        {
            _appDbContext.Players.Update(player);
        }

        public void RemovePlayer(Players player)
        {
            _appDbContext.Players.Remove(player);
        }

        public void RemoveAllPlayers(List<Players> players)
        {
            foreach (var player in players)
            {
                _appDbContext.Players.Remove(player);
            }
        }
    }
}
