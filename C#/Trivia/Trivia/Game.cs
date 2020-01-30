using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private Players players = new Players();
        private Dictionary<int, string> _categories =
            new Dictionary<int, string>
            {
                { 0, "Pop"},
                { 1, "Science"},
                { 2, "Sports"},
                { 3, "Rock"}
            };

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool Add(String playerName)
        {
            players.Add(new Player(playerName));
            return true;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(players.CurrentPlayer.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (players.CurrentPlayer.IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players.CurrentPlayer.Name + " is getting out of the penalty box");
                    players.CurrentPlayer.Move(roll);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(players.CurrentPlayer.Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                players.CurrentPlayer.Move(roll);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }


        private String CurrentCategory() =>
            _categories[players.CurrentPlayer.Place % 4];
        
        public bool WasCorrectlyAnswered()
        {
            if (players.CurrentPlayer.IsInPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    players.CurrentPlayer.WinAGoldCoin();
                    bool winner = players.CurrentPlayer.HasNotWon();
                    players.StartNextPlayerTurn();

                    return winner;
                }
                else
                {
                    players.StartNextPlayerTurn();
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                players.CurrentPlayer.WinAGoldCoin();
                bool winner = players.CurrentPlayer.HasNotWon();
                players.StartNextPlayerTurn();
                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players.CurrentPlayer.Name + " was sent to the penalty box");
            players.CurrentPlayer.GoToPenaltyBox();
            players.StartNextPlayerTurn();
            return true;
        }
    }
}
