using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();

        private readonly int[] _goldCoins = new int[6];

        private readonly bool[] _inPenaltyBox = new bool[6];

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast("Rock Question " + i);
            }
        }

        public bool Add(string playerName)
        {
            _players.Add(new Player(playerName));
            _goldCoins[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

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
            Console.WriteLine(_players[_currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[_currentPlayer].Name + " is getting out of the penalty box");
                    MoveAndAskQuestion(roll);
                }
                else
                {
                    Console.WriteLine(_players[_currentPlayer].Name + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                MoveAndAskQuestion(roll);
            }
        }

        private void MoveAndAskQuestion(int roll)
        {
            _players[_currentPlayer].Move(roll);
            Console.WriteLine(_players[_currentPlayer].Name
                              + "'s new location is "
                              + _players[_currentPlayer].Location);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }

            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            if (_players[_currentPlayer].Location % 4 == 0) return "Pop";
            if (_players[_currentPlayer].Location % 4 == 1) return "Science";
            if (_players[_currentPlayer].Location % 4 == 2) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    WinAGoldCoin();
                }

                return SwitchToNextPlayer();
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                WinAGoldCoin();

                return SwitchToNextPlayer();
            }
        }

        private void WinAGoldCoin()
        {
            _goldCoins[_currentPlayer]++;
            Console.WriteLine(_players[_currentPlayer].Name
                              + " now has "
                              + _goldCoins[_currentPlayer]
                              + " Gold Coins.");
        }

        private bool SwitchToNextPlayer()
        {
            var winner = DidPlayerWin();
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return winner;
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer].Name + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            return SwitchToNextPlayer();
        }

        private bool DidPlayerWin()
        {
            return _goldCoins[_currentPlayer] == 6;
        }
    }
}
