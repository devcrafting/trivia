using System;
using System.Collections.Generic;

namespace Trivia
{
    internal class AllPlayers
    {
        private readonly List<Player> _players = new List<Player>();
        private int _currentPlayer;
        public Player CurrentPlayer => _players[_currentPlayer];

        public void Add(Player player)
        {
            _players.Add(player);

            Console.WriteLine(player.Name + " was added");
            Console.WriteLine("They are player number " + _players.Count);
        }

        public bool SwitchToNextPlayer()
        {
            var winner = CurrentPlayer.DidPlayerWin();
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return winner;
        }
    }
}