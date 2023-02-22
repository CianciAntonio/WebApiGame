using DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repository
{
    public interface IGamesRepo : IRepository<Games>
    {
        Task<List<Games>> GetAllGamesAsync();
        Task<Games> GetGameAsync(string id);
        Task AddGameAsync(Games game);
        void RemoveGame(Games game);
    }
}
