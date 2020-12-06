using NFluent;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Fact]
        public void Should_fail()
        {
            Check.That(true).IsFalse();
        }
    }
}