using DataContext.Repository;

namespace DataContext.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IMatchesRepo MatchesRepo { get; }
        public IPlayersRepo PlayersRepo { get; }
        public IGamesRepo GamesRepo { get; }

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext= appDbContext;
            MatchesRepo = new MatchesRepo(_appDbContext);
            PlayersRepo= new PlayersRepo(_appDbContext);
            GamesRepo= new GamesRepo(_appDbContext);
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
        public async void Dispose() 
        {
            await _appDbContext.DisposeAsync();
        }
    }
}
