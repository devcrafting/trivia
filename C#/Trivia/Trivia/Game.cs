using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly AllPlayers _players = new AllPlayers();

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
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players.CurrentPlayer.Name + " is getting out of the penalty box");
                    MoveAndAskQuestion(roll);
                }
                else
                {
                    Console.WriteLine(_players.CurrentPlayer.Name + " is not getting out of the penalty box");
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
            _players.CurrentPlayer.Move(roll);
            var location = _players.CurrentPlayer.Location;
            var question = DrawQuestion(location);
            Console.WriteLine(_players.CurrentPlayer.Name
                              + "'s new location is "
                              + location);
            Console.WriteLine("The category is " + question.Category);
            Console.WriteLine(question.Text);
        }

        private Question DrawQuestion(int location)
        {
            Question question = null;
            if (location % 4 == 0)
            {
                question = new Question("Pop", _popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            else if (location % 4 == 1)
            {
                question = new Question("Science", _scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            else if (location % 4 == 2)
            {
                question = new Question("Sports", _sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            else
            {
                question = new Question("Rock", _rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
            return question;
        }

        public bool WasCorrectlyAnswered()
        {
            if (_players.CurrentPlayer.IsInPenaltyBox)
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players.CurrentPlayer.WinAGoldCoin();
                }

                return _players.SwitchToNextPlayer();
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                _players.CurrentPlayer.WinAGoldCoin();

                return _players.SwitchToNextPlayer();
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players.CurrentPlayer.Name + " was sent to the penalty box");
            _players.CurrentPlayer.SendToPenaltyBox();

            return _players.SwitchToNextPlayer();
        }
    }
}
