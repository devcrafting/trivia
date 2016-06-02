using NUnit.Framework;
using UglyTrivia;

namespace Trivia.Tests
{
    [TestFixture]
    public class Le_jeu_devrait
    {
        [Test]
        public void NUnitIsWorking()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void se_terminer_après_6_bonnes_réponses_d_un_joueur_quand_il_faut_6_points_pour_gagner()
        {
            // Arrange
            var le_jeu = new Game();
            le_jeu.add("toto");

            // Action
            le_jeu.wasCorrectlyAnswered();
            le_jeu.wasCorrectlyAnswered();
            le_jeu.wasCorrectlyAnswered();
            le_jeu.wasCorrectlyAnswered();
            le_jeu.wasCorrectlyAnswered();
            var se_terminer_après_6_bonnes_réponses_d_un_joueur = !le_jeu.wasCorrectlyAnswered();

            // Asserts
            Assert.IsTrue(se_terminer_après_6_bonnes_réponses_d_un_joueur);
        }

        [Test]
        public void se_terminer_après_2_bonnes_réponses_d_un_joueur_quand_il_faut_2_points_pour_gagner()
        {
            // Arrange
            var quand_il_faut_2_points_pour_gagner = 2;
            var le_jeu = new Game(quand_il_faut_2_points_pour_gagner);
            le_jeu.add("toto");

            // Action
            le_jeu.wasCorrectlyAnswered();
            var se_terminer_après_2_bonnes_réponses_d_un_joueur = !le_jeu.wasCorrectlyAnswered();

            // Asserts
            Assert.IsTrue(se_terminer_après_2_bonnes_réponses_d_un_joueur);
        }
    }
}
