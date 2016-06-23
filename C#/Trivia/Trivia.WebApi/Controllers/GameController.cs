using System;
using System.Collections.Generic;
using System.Web.Http;
using UglyTrivia;

namespace Trivia.WebApi.Controllers
{
    public class GameController : ApiController
    {
        private static Dictionary<string, Game> gamesInProgress = new Dictionary<string, Game>();

        [HttpGet]
        public string New()
        {
            var gameGuid = Guid.NewGuid().ToString();
            gamesInProgress[gameGuid] = new Game(6, new Questions(new [] { "Sports", "Rock", "Science", "Pop"}));
            return gameGuid;
        }
    }
}
