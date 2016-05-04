using System;
using System.Collections.Generic;
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

        //[Fact]
        //public void AllowToDefineQuestionsCategories()
        //{
        //    var game = NewGame(6, new Questions(new [] { "Pop", "Rock", "Science", "Sports" }));
        //}

        private static Game NewGame(int nbPursesToWin)
        {
            var game = new Game(nbPursesToWin);
            game.add("Joe");
            return game;
        }
    }
}
