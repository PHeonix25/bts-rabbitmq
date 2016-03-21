using Xunit;

namespace BehindTheScenes.Tests
{
    public class XUnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(true, true);
        }

        [Theory]
        [InlineData(true)]
        public void PassingTestWithInput(bool input)
        {
            Assert.Equal(true, input);
        }
    }
}