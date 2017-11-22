using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly Players _players = new Players();
        private readonly Questions _questions = new Questions();
        
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
        }

        public bool IsPlayable()
        {
            return (_players.HowManyPlayers() >= 2);
        }

        public bool Add(String playerName)
        {
            _players.Add(new Player(playerName));
            return true;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(_players.CurrentPlayer.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_players.CurrentPlayer.IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players.CurrentPlayer.Name + " is getting out of the penalty box");
                    _players.CurrentPlayer.Move(roll);
                    _questions.AskQuestion(_players.CurrentPlayer.Location);
                }
                else
                {
                    Console.WriteLine(_players.CurrentPlayer.Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                _players.CurrentPlayer.Move(roll);
                _questions.AskQuestion(_players.CurrentPlayer.Location);
            }

        }

        public bool WasCorrectlyAnswered()
        {
            if (_players.CurrentPlayer.IsInPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players.CurrentPlayer.WinAGoldCoin();

                    bool winner = DidPlayerWin();
                    _players.EndPlayerTurn();

                    return winner;
                }
                else
                {
                    _players.EndPlayerTurn();
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _players.CurrentPlayer.WinAGoldCoin();

                bool winner = DidPlayerWin();
                _players.EndPlayerTurn();

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            _players.CurrentPlayer.GoToPenaltyBox();
            _players.EndPlayerTurn();
            return true;
        }


        private bool DidPlayerWin()
        {
            return !_players.CurrentPlayer.IsWinner;
        }
    }
}
