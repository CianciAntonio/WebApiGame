using DataContext.Models;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Repository
{
    public class MatchesRepo : Repository<Matches>, IMatchesRepo
    {
        private AppDbContext _appDbContext;

        public MatchesRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<List<Matches>> GetAllMatchesAsync() 
        {
            var matches = await _appDbContext.Matches
                .AsNoTracking()
                .Include(y => y.player)
                .Include(x => x.game)
                .ToListAsync();

            return matches;
        }

        public async Task<Matches> GetMatchAsync(string id)
        {
            Matches? match = await _appDbContext.Matches
                .AsNoTracking()
                .Include(y => y.player)
                .Include(x => x.game)
                .FirstOrDefaultAsync(x => x.Id == id);

            return match;
        }

        public async Task AddMatchAsync(Matches match)
        {
            await _appDbContext.Matches.AddAsync(match);
        }

        public void RemoveMatch(Matches match)
        {
            _appDbContext.Matches.Remove(match);
        }
    }
}
