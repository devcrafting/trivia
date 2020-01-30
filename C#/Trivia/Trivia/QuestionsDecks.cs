using System;
using System.Collections.Generic;

namespace Trivia
{
    internal class QuestionsDecks
    {
        private Dictionary<string, LinkedList<string>> _questionsDeckByCategory = new Dictionary<string, LinkedList<string>>();

        public QuestionsDecks()
        {
            _questionsDeckByCategory["Pop"] = new LinkedList<string>();
            _questionsDeckByCategory["Science"] = new LinkedList<string>();
            _questionsDeckByCategory["Sports"] = new LinkedList<string>();
            _questionsDeckByCategory["Rock"] = new LinkedList<string>();
            for (var i = 0; i < 50; i++)
            {
                _questionsDeckByCategory["Pop"].AddLast("Pop Question " + i);
                _questionsDeckByCategory["Science"].AddLast(("Science Question " + i));
                _questionsDeckByCategory["Sports"].AddLast(("Sports Question " + i));
                _questionsDeckByCategory["Rock"].AddLast("Rock Question " + i);
            }
        }

        public void AskQuestionFor(string currentCategory)
        {
            var questionsDeck = _questionsDeckByCategory[currentCategory];
            Console.WriteLine(questionsDeck.First.Value);
            questionsDeck.RemoveFirst();
        }
    }
}