using System.IO;
using System.Linq;
using FluentAssertions;
using Implementation;
using Xunit;

namespace Tests
{
    public class Day14Tests
    {
        [Theory]
        [InlineData(11, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 73)]
        [InlineData(101, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 101)]
        [InlineData(0, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 64)]
        [InlineData(0, "100XX1111X0011100X01000X010000000101", 36387750917)]
        public void ApplyValueV1_ShouldGiveCorrectValue_GivenSpecificMask(ulong value, string mask, ulong expected)
        {
            var sut = new Memory();

            var actual = sut.ApplyValueV1(1, mask, value);

            actual.Should().Be(expected);
        }

        [Fact]
        public void ParseData_ShouldReturnMask_WhenInputIsMask()
        {
            var actual = DockingDataParser.ParseV1("mask = 110100X1X01011X01X0X000111X00XX1010X");
            actual.Mask.Should().NotBeNull();
        }

        [Fact]
        public void ParseData_ShouldReturnAddressAndValue_WhenInputIsMemoryAssignment()
        {
            var actual = DockingDataParser.ParseV1("mem[29267] = 4155");
            actual.Address.Should().Be(29267);
            actual.Value.Should().Be(4155);
        }

        [Fact]
        public void CalculateSumV1()
        {
            var input = File.ReadAllLines("Inputs\\day14.txt");
            var memory = new Memory();
            Memory.ValueMask valueMask = null;

            foreach (var value in input.Where(i => !string.IsNullOrWhiteSpace(i)))
            {
                var parsed = DockingDataParser.ParseV1(value);
                if (parsed.Mask != null) valueMask = parsed.Mask;
                else memory.ApplyValueV1(parsed.Address, valueMask, parsed.Value);
            }

            ulong result = 0;
            foreach (var value in memory.MemoryContents.Values)
            {
                result += value;
            }
            result.Should().Be(17934269678453);
        }

        [Theory]
        [InlineData(42, "000000000000000000000000000000X1001X", 4)]
        [InlineData(26, "00000000000000000000000000000000X0XX", 8)]
        [InlineData(26, "000000000000000000000000000000000000", 1)]
        public void ApplyValueV2_ShouldReturnAppliedAddresses_WhenGivenInput(ulong address, string mask, int expectedAddressCount)
        {
            var sut = new Memory();

            var actual = sut.ApplyValueV2(address, mask, 0);

            actual.Length.Should().Be(expectedAddressCount);
            sut.MemoryContents.Count.Should().Be(expectedAddressCount);
        }

        [Fact]
        public void CalculateSumV2()
        {
            var input = File.ReadAllLines("Inputs\\day14.txt");
            var memory = new Memory();
            Memory.AddressMask valueMask = null;

            foreach (var value in input.Where(i => !string.IsNullOrWhiteSpace(i)))
            {
                var parsed = DockingDataParser.ParseV2(value);
                if (parsed.Mask != null) valueMask = parsed.Mask;
                else memory.ApplyValueV2(parsed.Address, valueMask, parsed.Value);
            }

            ulong result = 0;
            foreach (var value in memory.MemoryContents.Values)
            {
                result += value;
            }
            result.Should().Be(3440662844064);
        }

    }
}