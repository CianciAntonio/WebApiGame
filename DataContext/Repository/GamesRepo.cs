using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Sources;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataContext.Repository
{
    public class GamesRepo : Repository<Games>, IGamesRepo
    {
        private AppDbContext _appDbContext;

        public GamesRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable> GetGamesRecord()
        {
            //Query Sql:

            //SELECT g.Name, m.score, MAX(date) as Record_Date
            //FROM Games g
            //LEFT JOIN Matches m ON g.Id = m.idGame
            //AND m.score = (SELECT MAX(score)
            //            FROM Matches
            //            WHERE idGame = M.idGame)
            //GROUP BY g.Name, m.score

            //ChatGPT
            //var query = from g in _appDbContext.Games
            //            join m in _appDbContext.Matches on g.Id equals m.idGame into mj
            //            from subM in mj.Where(x => x.score == mj.Max(y => y.score)).DefaultIfEmpty()
            //            group subM by new { g.Name, subM.score } into grp
            //            select new
            //            {
            //                grp.Key.Name,
            //                Score = grp.Key.score == 0 ? null : (int?)grp.Key.score,
            //                Record_Date = grp.Max(x => x != null ? x.date : (DateTime?)null)
            //            };

            //Mio tentativo (che alla fine è uguale a chatGPT)
            var recordGames =   from g in _appDbContext.Games
                                join m in _appDbContext.Matches
                                on g.Id equals m.idGame into matchesGroup
                                from mg in matchesGroup.DefaultIfEmpty()
                                where mg.score == (from m in _appDbContext.Matches
                                                   where m.idGame == mg.idGame
                                                   select m.score).Max()
                                group mg by new { g.Name, mg.score } into g
                                select new
                                {
                                    g.Key.Name,
                                    Score = g.Key.score == 0 ? null : (int?)g.Key.score,
                                    Record_Date = g.Max(x => x != null ? x.date : (DateTime?)null)
                                };

            return recordGames;
        }

        public async Task<List<Games>> GetAllGamesAsync()
        {
            var games = await _appDbContext.Games
                .AsNoTracking()
                .Include(y => y.match)
                .ToListAsync();

            return games;
               
        }

        public async Task<Games> GetGameAsync(string id)
        {
            Games? game = await _appDbContext.Games
                .AsNoTracking()
                .Include(y => y.match)
                .FirstOrDefaultAsync(x => x.Id == id);

            return game;
        }

        public async Task AddGameAsync(Games game)
        {
            await _appDbContext.Games.AddAsync(game);
        }

        public void RemoveGame(Games game)
        {
            _appDbContext.Remove(game);
        }
    }
}
