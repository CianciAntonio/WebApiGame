﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.ModelResponse
{
    public class GameResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<MatchesResponse> matches { get; set; }
    }
}