using System;
using System.Collections.Generic;

namespace Trivia
{
    internal class Players
    {
        private readonly List<Player> _players = new List<Player>();
        public Player CurrentPlayer { get; private set; }

        public void Add(Player player)
        {
            _players.Add(player);

            if (_players.Count == 1)
                CurrentPlayer = player;

            Console.WriteLine(player.Name + " was added");
            Console.WriteLine("They are player number " + _players.Count);
        }

        public void StartNextPlayerTurn()
        {
            CurrentPlayer = _players[(_players.IndexOf(CurrentPlayer) + 1) % _players.Count];
        }
    }
}