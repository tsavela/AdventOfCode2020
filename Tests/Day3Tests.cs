using System.IO;
using System.Linq;
using FluentAssertions;
using Implementation;
using Xunit;

namespace Tests
{
    public class Day3Tests
    {
        [Fact]
        public void CalculateNumberOfTrees_ShouldReturnTotalNumberOfTrees_GivenSmallMap()
        {
            var map = new[]
            {
                "#.",
                ".#"
            };
            var sut = new TobogganTrajectory();

            var actual = sut.CalculateNumberOfTrees(map, (1, 1));

            actual.Should().Be(2);
        }

        [Fact]
        public void CalculateNumberOfTrees_ShouldReturnTotalNumberOfTrees_GivenLongMap()
        {
            var map = new[]
            {
                "#.",
                ".#",
                "#.",
                ".#"
            };
            var sut = new TobogganTrajectory();

            var actual = sut.CalculateNumberOfTrees(map, (1, 1));

            actual.Should().Be(4);
        }

        [Fact]
        public void CalculateNumberOfTrees_ShouldReturnTotalNumberOfHits_GivenProblemInputAndTrajectory3And1()
        {
            var map = File.ReadAllLines("Inputs\\day3.txt");
            var sut = new TobogganTrajectory();

            var actual = sut.CalculateNumberOfTrees(map, (3, 1));

            actual.Should().Be(195);
        }

        [Fact]
        public void CalculateNumberOfTrees_ShouldReturnTotalNumberOfTrees_GivenProblemInputAndMultipleTrajectories()
        {
            var map = File.ReadAllLines("Inputs\\day3.txt");
            var sut = new TobogganTrajectory();
            var trajectories = new[] {(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)};

            var actual = trajectories.Select(trajectory => sut.CalculateNumberOfTrees(map, trajectory));

            actual.Aggregate((total, routeTrees) => total * routeTrees).Should().Be(3772314000);
        }
    }
}