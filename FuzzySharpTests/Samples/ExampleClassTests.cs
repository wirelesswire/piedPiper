namespace FuzzySharpTests.Samples
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldProperlyAddTwoNumbers()
        {
            //Arrange
            int a = 5;
            int b = 10;

            //Act
            int result = a + b;

            //Assert
            Assert.That(result, Is.EqualTo(15));
        }
    }
}