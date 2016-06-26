using System;
using System.Collections.Generic;
using System.Web.Http;
using Trivia.Domain;
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

        [HttpPost]
        public void Stop(string guid)
        {
            
        }

        [HttpPost]
        public void AddPlayer(string id, PlayerName playerName)
        {
            gamesInProgress[id].add(playerName.Name);
        }
    }

    public class PlayerName
    {
        public string Name { get; set; }
    }
}
