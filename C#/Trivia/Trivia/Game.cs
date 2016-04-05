using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {
        List<Player> players = new List<Player>();

        private Dictionary<int, QuestionStack> questionsAccordingPosition = new Dictionary<int, QuestionStack>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            questionsAccordingPosition.Add(0, new QuestionStack("Pop"));
            questionsAccordingPosition.Add(1, new QuestionStack("Science"));
            questionsAccordingPosition.Add(2, new QuestionStack("Sports"));
            questionsAccordingPosition.Add(3, new QuestionStack("Rock"));
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            players.Add(new Player(playerName));
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            Console.WriteLine(players[currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (players[currentPlayer].IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[currentPlayer].Name + " is getting out of the penalty box");
                    players[currentPlayer].Move(roll);

                    Console.WriteLine(players[currentPlayer].Name
                            + "'s new location is "
                            + players[currentPlayer].Place);
                    Console.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer].Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {
                players[currentPlayer].Move(roll);
                Console.WriteLine(players[currentPlayer].Name
                        + "'s new location is "
                        + players[currentPlayer].Place);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }
        }

        private void askQuestion()
        {
            questionsAccordingPosition[players[currentPlayer].Place % 4].AskNextQuestion();
        }


        private String currentCategory()
        {
            return questionsAccordingPosition[players[currentPlayer].Place % 4].CategoryName;
        }

        public bool wasCorrectlyAnswered()
        {
            if (players[currentPlayer].IsInPenaltyBox)
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    players[currentPlayer].WinOnePurse();
                    Console.WriteLine(players[currentPlayer].Name
                            + " now has "
                            + players[currentPlayer].Purses
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                players[currentPlayer].WinOnePurse();
                Console.WriteLine(players[currentPlayer].Name
                        + " now has "
                        + players[currentPlayer].Purses
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer].Name + " was sent to the penalty box");
            players[currentPlayer].GoToPenaltyBox();

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }

        private bool didPlayerWin()
        {
            return !(players[currentPlayer].Purses == 6);
        }
    }
}
