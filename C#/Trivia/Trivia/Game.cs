using System;
using System.Collections.Generic;

namespace UglyTrivia
{
    public class Game
    {
        private readonly int _nbPursesToWin;
        private readonly List<Player> players = new List<Player>();

        private readonly Questions _questions = new Questions();
        
        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public Game(int nbPursesToWin)
        {
            _nbPursesToWin = nbPursesToWin;
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
                    _questions.AskQuestion(currentCategory());
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
                _questions.AskQuestion(currentCategory());
            }
        }


        private String currentCategory()
        {
            if (players[currentPlayer].Place % 4 == 0) return "Pop";
            if (players[currentPlayer].Place % 4 == 1) return "Science";
            if (players[currentPlayer].Place % 4 == 2) return "Sports";
            return "Rock";
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
            return !(players[currentPlayer].Purses == _nbPursesToWin);
        }
    }
}
