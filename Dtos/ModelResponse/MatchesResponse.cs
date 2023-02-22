using DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.ModelResponse
{
    public class MatchesResponse
    {
        public string Id { get; set; }
        public string PlayerName { get; set; }
        public string GameName { get; set; }
        public float score { get; set; }
        public DateTime date { get; set; }
    }
}
