using DataContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMatchesRepo MatchesRepo { get; }
        IPlayersRepo PlayersRepo { get; }
        IGamesRepo GamesRepo { get; }

        Task SaveAsync();
        void Dispose();
    }
}
