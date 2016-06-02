using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    public class Questions
    {
        private Dictionary<string, LinkedList<string>> _questionsByCategory = new Dictionary<string, LinkedList<string>>();

        public Questions()
        {
            _questionsByCategory["Pop"] = new LinkedList<string>();
            _questionsByCategory["Science"] = new LinkedList<string>();
            _questionsByCategory["Sports"] = new LinkedList<string>();
            _questionsByCategory["Rock"] = new LinkedList<string>();
            for (int i = 0; i < 50; i++)
            {
                _questionsByCategory["Pop"].AddLast("Pop Question " + i);
                _questionsByCategory["Science"].AddLast(("Science Question " + i));
                _questionsByCategory["Sports"].AddLast(("Sports Question " + i));
                _questionsByCategory["Rock"].AddLast(CreateRockQuestion(i));
            }
        }

        public Questions(IEnumerable<string> categories, IExternalSource externalSource = null)
        {
            foreach (var category in categories)
            {
                if (externalSource == null)
                {
                    _questionsByCategory[category] = new LinkedList<string>();
                    for (int i = 0; i < 50; i++)
                    {
                        _questionsByCategory[category].AddLast(category + " Question " + i);
                    }
                }
                else
                {
                    _questionsByCategory[category] = externalSource.GetQuestions("category");
                }
            }
        }

        private string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public void AskQuestion(string category)
        {
            var questions = _questionsByCategory[category];
            Console.WriteLine(questions.First());
            questions.RemoveFirst();
        }

        public string GetCategoryAt(int place)
        {
            return _questionsByCategory.Keys.ElementAt(place % _questionsByCategory.Count);
        }
    }
}