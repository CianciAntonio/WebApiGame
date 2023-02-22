using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Players> GetPlayerAsync(string id)
        {
            Players? player = await _appDbContext.Players
                .AsNoTracking()
                .Include(y => y.matches)
                .FirstOrDefaultAsync(x => x.Id== id);

            return player;
        }

        public async Task AddPlayerAsync(Players player)
        {
            await _appDbContext.Players.AddAsync(player);
        }

        public void RemovePlayer(Players player)
        {
            _appDbContext.Players.Remove(player);
        }
    }
}
