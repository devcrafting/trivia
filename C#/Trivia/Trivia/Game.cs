using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();
        private Questions _questions = new Questions();
        
        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool Add(String playerName)
        {
            _players.Add(new Player(playerName));

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(_players[currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_players[currentPlayer].IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[currentPlayer].Name + " is getting out of the penalty box");
                    _players[currentPlayer].Move(roll);
                    _questions.AskQuestion(_players[currentPlayer].Location);
                }
                else
                {
                    Console.WriteLine(_players[currentPlayer].Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                _players[currentPlayer].Move(roll);
                _questions.AskQuestion(_players[currentPlayer].Location);
            }

        }

        public bool WasCorrectlyAnswered()
        {
            if (_players[currentPlayer].IsInPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players[currentPlayer].WinAGoldCoin();

                    bool winner = DidPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == _players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == _players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _players[currentPlayer].WinAGoldCoin();

                bool winner = DidPlayerWin();
                currentPlayer++;
                if (currentPlayer == _players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            _players[currentPlayer].GoToPenaltyBox();

            currentPlayer++;
            if (currentPlayer == _players.Count) currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return !_players[currentPlayer].IsWinner;
        }
    }
}
