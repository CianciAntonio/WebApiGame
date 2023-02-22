using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.ModelRequest
{
    public class MatchRequest
    {
        public string idPlayer { get; set; }
        public string idGame { get; set; }
        public float score { get; set; }
        public DateTime date { get; set; }
    }
}
