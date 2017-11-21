using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace Trivia.Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class GoldenMaster
    {
        [Fact]
        public void ShouldNotChange()
        {
            var stringWriter = new StringWriter();
            var previousConsoleOut = Console.Out;
            Console.SetOut(stringWriter);
            GameRunner.Main(null);
            Console.SetOut(previousConsoleOut);
            Approvals.Verify(stringWriter.ToString());
        }
    }
}
