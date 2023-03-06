using DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataContext.Repository
{
    public interface IPlayersRepo : IRepository<Players>
    {
        Task<IEnumerable> GetPlayersRecords();
        Task<List<Players>> GetAllPlayersAsync();
        Task<Players> GetPlayerAsync(string id);
        Task AddPlayerAsync(Players player);
        void UpdatePlayerAsync(Players player);
        void RemovePlayer(Players player);
        void RemoveAllPlayers(List<Players> players);
    }
}
