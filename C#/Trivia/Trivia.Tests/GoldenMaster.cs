using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using Xunit;

namespace Trivia.Tests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class GoldenMaster
    {
        [Test]
        public void CheckGoldenMasterNotModified()
        {
            var consoleOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            GameRunner.Main(null);

            Approvals.Verify(stringWriter.ToString());

            Console.SetOut(consoleOut);
        }
    }
}
