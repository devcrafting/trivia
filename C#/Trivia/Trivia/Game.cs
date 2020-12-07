using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly AllPlayers _players = new AllPlayers();

        private readonly Dictionary<int, LinkedList<Question>> _questions = new Dictionary<int, LinkedList<Question>>();

        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            _questions[0] = new LinkedList<Question>();
            _questions[1] = new LinkedList<Question>();
            _questions[2] = new LinkedList<Question>();
            _questions[3] = new LinkedList<Question>();
            for (var i = 0; i < 50; i++)
            {
                _questions[0].AddLast(new Question("Pop", "Pop Question " + i));
                _questions[1].AddLast(new Question("Science", "Science Question " + i));
                _questions[2].AddLast(new Question("Sports", "Sports Question " + i));
                _questions[3].AddLast(new Question("Rock", "Rock Question " + i));
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
            var question = _questions[location % 4].First();
            _questions[location % 4].RemoveFirst();
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
