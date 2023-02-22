namespace DataContext.Models
{
    public class Games : TimeStamps
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public List<Matches> match { get; set; }
    }
}