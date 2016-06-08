using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {

        private static bool notAWinner;

        public static void Main(String[] args)
        {
            for (var i = 0; i < 100; i++)
            {
                var questions = new Questions(new[] { "Pop", "Science", "Sports", "Rock" }, new QuestionFlatFile());
                var aGame = new Game(6, questions);

                aGame.add("Chet");
                aGame.add("Pat");
                aGame.add("Sue");

                var rand = new Random(i);

                do
                {
                    aGame.roll(rand.Next(5) + 1);

                    if (rand.Next(9) == 7)
                    {
                        notAWinner = aGame.wrongAnswer();
                    }
                    else
                    {
                        notAWinner = aGame.wasCorrectlyAnswered();
                    }
                } while (notAWinner);
            }
        }


    }

}

