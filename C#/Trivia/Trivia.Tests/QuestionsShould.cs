using System;
using System.Collections.Generic;
using System.IO;
using NFluent;
using UglyTrivia;
using Xunit;

namespace Trivia.Tests
{
    public class QuestionsShould
    {
        [Fact]
        public void AllowToDefine4Categories()
        {
            var questions = new Questions(new[] { "Pop", "Rock", "Science", "Sports" });
            var consoleOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            questions.AskQuestion("Pop");

            Check.That(stringWriter.ToString()).IsEqualTo("Pop Question 0" + Environment.NewLine);
            Console.SetOut(consoleOut);
        }

        [Fact]
        public void AllowToDefine5Categories()
        {
            var questions = new Questions(new[] { "Pop", "Rock", "Science", "Sports", "Cinema" });
            var consoleOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            questions.AskQuestion("Cinema");

            Check.That(stringWriter.ToString()).IsEqualTo("Cinema Question 0" + Environment.NewLine);
            Console.SetOut(consoleOut);
        }

        [Fact]
        public void AllowToGetQuestionsFromExternalSource()
        {
            var externalSource = new FakeExternalSource();
            var questions = new Questions(
                new[] { "Pop", "Rock", "Science", "Sports", "Cinema" },
                externalSource);
            var consoleOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            questions.AskQuestion("Sports");

            Check.That(stringWriter.ToString()).IsEqualTo("Qui est recordman du 100m?" + Environment.NewLine);
            Console.SetOut(consoleOut);
        }
    }

    public class FakeExternalSource : IExternalSource
    {
        public LinkedList<string> GetQuestions(string category)
        {
            var linkedList = new LinkedList<string>();
            linkedList.AddLast("Qui est recordman du 100m?");
            return linkedList;
        }
    }
}
