using System;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;

namespace Trivia.WebApi
{
    public class TriviaModule : NancyModule
    {
        private static Game _game;
        private static JsonEventDispatcher _eventDispatcher;

        public TriviaModule()
        {
            Post["/startGame"] = StartGame;
            Post["/rollDice"] = RollDice;
        }

        private dynamic RollDice(dynamic arg)
        {
            _eventDispatcher.Flush();
            var random = new Random();
            _game.Roll(random.Next(5) + 1);
            return JsonConvert.SerializeObject(_eventDispatcher.Events);
        }

        private dynamic StartGame(dynamic o)
        {
            var startGame = this.Bind<StartGame>();
            _eventDispatcher = new JsonEventDispatcher();
            var players = new Players(_eventDispatcher);
            foreach (var player in startGame.Players)
            {
                players.Add(player);
            }

            _game = new Game(players, new Questions(startGame.Categories, new GeneratedQuestions(), _eventDispatcher), _eventDispatcher);
            return JsonConvert.SerializeObject(_eventDispatcher.Events);
        }
    }
}
