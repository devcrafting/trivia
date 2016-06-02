using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using UglyTrivia;
using Xunit;

namespace Trivia.Tests
{
    public class GameShould
    {
        [Fact]
        public void AllowToDefineNbPurseToWin()
        {
            var nbPursesToWin = new Random().Next(1, 20);
            var game = NewGame(nbPursesToWin);
            var hasNotWon = true;
            for (var i = 0; i < nbPursesToWin; i++)
            {
                Check.That(hasNotWon).IsTrue();
                hasNotWon = game.wasCorrectlyAnswered();
            }
            Check.That(hasNotWon).IsFalse();
        }

        [Fact]
        public void AllowToDefineQuestionsCategories()
        {
            var game = NewGame(6, new Questions(new[] { "Pop", "Rock", "Science", "Sports" }));
            var consoleOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            game.roll(1);
            game.roll(1);
            game.roll(1);
            game.roll(1);

            var categories = stringWriter.ToString().Split(new [] {Environment.NewLine}, StringSplitOptions.None)
                .Where(x => x.StartsWith("The category is"))
                .Select(x => x.Replace("The category is ", ""));
            Check.That(categories)
                .ContainsExactly("Rock", "Science", "Sports", "Pop");

            Console.SetOut(consoleOut);
        }

        private static Game NewGame(int nbPursesToWin, Questions questions = null)
        {
            var game = new Game(nbPursesToWin, questions);
            game.add("Joe");
            return game;
        }
    }
}
