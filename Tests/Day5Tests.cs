using System.IO;
using System.Linq;
using FluentAssertions;
using Implementation;
using Xunit;

namespace Tests
{
    public class Day5Tests
    {
        [Theory]
        [InlineData("BFFFBBFRRR", 70, 7, 567)]
        [InlineData("FFFBBBFRRR", 14, 7, 119)]
        [InlineData("BBFFBBFRLL", 102, 4, 820)]
        public void Properties_ShouldBeCalculatedCorrectly_GivenValidInput(string definition, int expectedRow, int expectedColumn, int expectedId)
        {
            var sut = new Seat(definition);

            sut.Row.Should().Be(expectedRow);
            sut.Column.Should().Be(expectedColumn);
            sut.Id.Should().Be(expectedId);
        }

        [Fact]
        public void CalculateMaxIdFromInput()
        {
            var input = File.ReadLines("Inputs\\day5.txt");

            var max = input.Max(seatDefinition => new Seat(seatDefinition).Id);

            max.Should().Be(976);
        }

        [Fact]
        public void FindMySeat()
        {
            var input = File.ReadLines("Inputs\\day5.txt");
            var occupiedSeats = input.Select(seatDefinition => new Seat(seatDefinition).Id).OrderBy(id => id).ToArray();
            var possibleSeatIds = Enumerable.Range(0, 128 * 8);

            var freeSeatId = possibleSeatIds.Single(seatId =>
                !occupiedSeats.Contains(seatId) && occupiedSeats.Contains(seatId - 1) && occupiedSeats.Contains(seatId + 1));

            freeSeatId.Should().Be(685);
        }
    }
}