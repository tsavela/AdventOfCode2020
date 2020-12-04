using System.IO;
using System.Linq;
using FluentAssertions;
using Implementation;
using Xunit;

namespace Tests
{
    public class Day1Tests
    {
        [Fact]
        public void FindPairWithSum_ShouldFindCorrectPair_GivenSumThatHasMatchingPair()
        {
            var sut = new ExpenseReportCalculator();

            var actual = sut.FindPairWithSum(Enumerable.Range(1, 10).ToArray(), 7);

            actual.Should().Be((1, 6));
        }

        [Fact]
        public void FindPairWithSum_ShouldFindCorrectPair_GivenProblemInput()
        {
            var input = File.ReadAllLines("Inputs\\day1.txt").Select(int.Parse).ToArray();
            var sut = new ExpenseReportCalculator();

            var actual = sut.FindPairWithSum(input, 2020);

            actual.Should().Be((1245, 775));
            (actual.Value.first * actual.Value.second).Should().Be(964875);
        }

        [Fact]
        public void FindTripletWithSum_ShouldFindCorrectTriplet_GivenSumThatHasMatchingPair()
        {
            var sut = new ExpenseReportCalculator();

            var actual = sut.FindTripletWithSum(Enumerable.Range(1, 10).ToArray(), 7);

            actual.Should().Be((1, 2, 4));
        }

        [Fact]
        public void FindTripletWithSum_ShouldFindCorrectTriplet_GivenProblemInput()
        {
            var input = File.ReadAllLines("Inputs\\day1.txt").Select(int.Parse).ToArray();
            var sut = new ExpenseReportCalculator();

            var actual = sut.FindTripletWithSum(input, 2020);

            actual.Should().Be((1104, 715, 201));
            (actual.Value.first * actual.Value.second * actual.Value.third).Should().Be(158661360);
        }
    }
}