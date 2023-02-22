using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Models
{
    public class Matches : TimeStamps
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string idPlayer { get; set; }
        public string idGame { get; set; }
        public float score { get; set; }
        public DateTime date { get; set; }

        public Players player { get; set; }
        public Games game { get; set; }
    }
}