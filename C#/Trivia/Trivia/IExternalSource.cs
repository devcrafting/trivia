using System.Collections.Generic;

namespace UglyTrivia
{
    public interface IExternalSource
    {
        LinkedList<string> GetQuestions(string category);
    }
}