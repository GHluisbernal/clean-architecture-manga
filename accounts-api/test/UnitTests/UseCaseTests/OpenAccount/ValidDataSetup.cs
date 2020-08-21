namespace UnitTests.UseCaseTests.OpenAccount
{
    using Xunit;

    internal sealed class ValidDataSetup : TheoryData<string, string, string>
    {
        public ValidDataSetup()
        {
            this.Add("John", "Doe", "198608174444");
            this.Add("Mary", "Jane", "198608174444");
            this.Add("Ivan", "Doe", "198608174444");
        }
    }
}
