using System.IO;
using System.Linq;
using FluentAssertions;
using Implementation;
using Xunit;

namespace Tests
{
    public class Day6Tests
    {
        [Fact]
        public void CalculateAnswersSomeoneAnsweredYesOn_ShouldGiveCorrectValue_GivenSpecificValidInput()
        {
            var input = new[] {"abcx", "abcy", "abcz"};
            var sut = new CustomsFormCalculator();

            var actual = sut.CalculateAnswersSomeoneAnsweredYesOn(input);

            actual.Should().Be("abcxyz");
        }

        [Fact]
        public void SumOfCalculateAnswersSomeoneAnsweredYesOn()
        {
            var input = File.ReadAllText("Inputs\\day6.txt");
            var inputGroups = input.Split("\n\n");
            var sut = new CustomsFormCalculator();

            var sum = inputGroups.Sum(inputGroup => sut.CalculateAnswersSomeoneAnsweredYesOn(inputGroup.Split('\n')).Length);

            sum.Should().Be(6504);
        }

        [Fact]
        public void CalculateAnswersEveryoneAnsweredYesOn_ShouldGiveCorrectValue_GivenSpecificValidInput()
        {
            var input = new[] {"ab", "ac"};
            var sut = new CustomsFormCalculator();

            var actual = sut.CalculateAnswersEveryoneAnsweredYesOn(input);

            actual.Should().Be("a");
        }

        [Fact]
        public void SumOfCalculateAnswersEveryoneAnsweredYesOn()
        {
            var input = File.ReadAllText("Inputs\\day6.txt");
            var inputGroups = input.Split("\n\n");
            var sut = new CustomsFormCalculator();

            var sum = inputGroups.Sum(inputGroup => sut.CalculateAnswersEveryoneAnsweredYesOn(inputGroup.Split('\n')).Length);

            sum.Should().Be(3351);
        }
    }
}