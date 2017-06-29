using System.Collections.Generic;

namespace Trivia.WebApi
{
    internal class StartGame
    {
        public List<string> Players { get; set; }
        public List<string> Categories { get; set; }
    }
}