using DataContext.Models;

namespace Dtos.ModelResponse
{
    public class PlayerResponse
    {
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<MatchesResponse> matches { get; set; }
    }
}
