using System.Collections.Generic;
using System.IO;

namespace UglyTrivia
{
    class QuestionFlatFile : IExternalSource
    {
        public LinkedList<string> GetQuestions(string category)
        {
            var readAllLines = File.ReadAllLines("bin\\Debug\\" + category + ".txt");
            var questions = new LinkedList<string>();
            foreach (var line in readAllLines)
            {
                questions.AddLast(line);
            }
            return questions;
        }
    }
}