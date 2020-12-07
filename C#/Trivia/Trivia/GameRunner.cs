using System;

namespace Trivia
{
    public class GameRunner
    {
        private static bool _isWinner;

        public static void Main(string[] args)
        {
            var rand = new Random();
            PlayGame(rand);
        }

        public static void PlayGame(Random rand)
        {
            var aGame = new Game();

            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            do
            {
                aGame.Roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    _isWinner = aGame.WrongAnswer();
                }
                else
                {
                    _isWinner = aGame.WasCorrectlyAnswered();
                }
            } while (!_isWinner);
        }
    }
}