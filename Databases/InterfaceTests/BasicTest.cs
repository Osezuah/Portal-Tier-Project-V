namespace InterfaceTests
{
    // The testing works when Solution file is set to a "Class Library" and not "Console Application" because Program.cs doesn't have a main function yet.
    public class BasicTest
    {
        [Fact]
        public void Test1Plus1_Successful()
        {
            // Example test so show that XUnit is working with the project
            // Arrange
            int rhs = 1;
            int lhs = 1;

            //Act
            int result = rhs + lhs;

            //Assert 
            Assert.Equal(2, result);
        }
    }
}