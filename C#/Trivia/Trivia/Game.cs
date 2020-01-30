using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly Players players = new Players();
        private readonly QuestionsDecks _questionsDecks = new QuestionsDecks();
        private readonly Dictionary<int, string> _categories =
            new Dictionary<int, string>
            {
                { 0, "Pop"},
                { 1, "Science"},
                { 2, "Sports"},
                { 3, "Rock"}
            };

        bool isGettingOutOfPenaltyBox;

        public Game()
        {
        }

        public void Add(string playerName)
        {
            players.Add(new Player(playerName));
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
            _questionsDecks.AskQuestionFor(CurrentCategory());
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
