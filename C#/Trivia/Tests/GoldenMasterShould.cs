using System;
using System.IO;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Trivia;
using Xunit;

namespace Tests
{
    public class GoldenMasterShould
    {
        [Fact]
        [UseReporter(typeof(QuietReporter))]
        public void Not_change()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var random = new Random(0);
            Enumerable.Range(1, 15)
                .ForEach(_ => GameRunner.PlayGame(random));
            Approvals.Verify(stringWriter.ToString());
        }
    }
}