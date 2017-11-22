﻿using System;

namespace Trivia
{
    public class GameRunner
    {
        private static bool notAWinner;

        public static void Main(String[] args)
        {
            for (var i = 0; i < 10; i++)
            {
                var players = new Players();
                players.Add("Chet");
                players.Add("Pat");
                players.Add("Sue");
                var aGame = new Game(players, new Questions());

                Random rand = new Random(i);

                do
                {
                    aGame.Roll(rand.Next(5) + 1);

                    if (rand.Next(9) == 7)
                    {
                        notAWinner = aGame.WrongAnswer();
                    }
                    else
                    {
                        notAWinner = aGame.WasCorrectlyAnswered();
                    }
                } while (notAWinner);
            }
        }
    }
}

