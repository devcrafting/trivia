using System;
using Nancy;
using Nancy.ModelBinding;

namespace Trivia.WebApi
{
    public class TriviaModule : NancyModule
    {
        private static Game _game;
        private static WebEventDispatcher _eventDispatcher;

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
            return _eventDispatcher.Output;
        }

        private dynamic StartGame(dynamic o)
        {
            var startGame = this.Bind<StartGame>();
            _eventDispatcher = new WebEventDispatcher();
            var players = new Players(_eventDispatcher);
            foreach (var player in startGame.Players)
            {
                players.Add(player);
            }

            _game = new Game(players, new Questions(startGame.Categories, new GeneratedQuestions(), _eventDispatcher), _eventDispatcher);
            return _eventDispatcher.Output;
        }
    }
}
