using System.Text.Json.Serialization;

namespace DataContext.Models
{
    public class Players : TimeStamps
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<Matches> matches { get; set; }
    }
}