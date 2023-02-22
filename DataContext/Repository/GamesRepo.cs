using DataContext.Models;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repository
{
    public class GamesRepo : Repository<Games>, IGamesRepo
    {
        private AppDbContext _appDbContext;

        public GamesRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
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
