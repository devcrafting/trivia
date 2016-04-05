using System;
using System.Collections.Generic;
using System.Linq;

namespace UglyTrivia
{
    internal class QuestionStack
    {
        private LinkedList<string> questions = new LinkedList<string>();

        public QuestionStack(string categoryName)
        {
            CategoryName = categoryName;
            for (int i = 0; i < 50; i++)
            {
                questions.AddLast(CategoryName + " Question " + i);
            }
        }

        public string CategoryName { get; private set; }

        public void AskNextQuestion()
        {
            Console.WriteLine(questions.First());
            questions.RemoveFirst();
        }
    }
}