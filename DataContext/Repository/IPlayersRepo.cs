using DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repository
{
    public interface IPlayersRepo : IRepository<Players>
    {
        Task<List<Players>> GetAllPlayersAsync();
        Task<Players> GetPlayerAsync(string id);
        Task AddPlayerAsync(Players player);
        void RemovePlayer(Players player);
    }
}
