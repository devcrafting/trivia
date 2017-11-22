using System;
using System.Collections.Generic;

namespace Trivia
{
    public class Players
    {
        private readonly List<Player> _players = new List<Player>();

        public Player CurrentPlayer { get; private set; }

        public void Add(string playerName)
        {
            var player = new Player(playerName);
            _players.Add(player);
            if (CurrentPlayer == null)
            {
                CurrentPlayer = player;
            }

            Console.WriteLine($"{playerName} was added");
            Console.WriteLine($"They are player number {_players.Count}");
        }

        public int HowManyPlayers() => _players.Count;

        public void EndPlayerTurn()
        {
            var nextPlayerIndex = (_players.IndexOf(CurrentPlayer) + 1) % _players.Count;
            CurrentPlayer = _players[nextPlayerIndex];
        }
    }
}