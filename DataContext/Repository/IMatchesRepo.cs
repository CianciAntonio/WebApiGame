using DataContext.Models;
namespace DataContext.Repository
{
    public interface IMatchesRepo : IRepository<Matches>
    {
        Task<List<Matches>> GetAllMatchesAsync();
        Task<Matches> GetMatchAsync(string id);
        Task AddMatchAsync(Matches match);
        void RemoveMatch(Matches match);
    }
}
