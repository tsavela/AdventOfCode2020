using System.IO;
using FluentAssertions;
using Implementation;
using Xunit;

namespace Tests
{
    public class Day2Tests
    {
        [Theory]
        [InlineData("1-3 a", "abcde")]
        [InlineData("2-9 c", "ccccccccc")]
        public void IsPasswordValid_ShouldReturnTrueForMaxMinValidationStrategy_WhenGivenValidPassword(string policy, string password)
        {
            var sut = new PasswordValidator(new MaxMinCharacterCountValidationStrategy());

            var actual = sut.IsPasswordValid(policy, password);

            actual.Should().BeTrue();
        }

        [Theory]
        [InlineData("1-3 b", "cdefg")]
        public void IsPasswordValid_ShouldReturnFalseForMaxMinValidationStrategy_WhenGivenInvalidPassword(string policy, string password)
        {
            var sut = new PasswordValidator(new MaxMinCharacterCountValidationStrategy());

            var actual = sut.IsPasswordValid(policy, password);

            actual.Should().BeFalse();
        }

        [Fact]
        public void FilterValidPasswords_ShouldReturnCorrectNumberOfItemsForMaxMinValidationStrategy_GivenProblemInput()
        {
            var input = File.ReadAllLines("Inputs\\day2.txt");
            var sut = new PasswordValidator(new MaxMinCharacterCountValidationStrategy());

            var actual = sut.FilterValidPasswords(input);

            actual.Length.Should().Be(636);
        }

        [Theory]
        [InlineData("1-3 a", "abcde")]
        public void IsPasswordValid_ShouldReturnTrueForPositionValidationStrategy_WhenGivenValidPassword(string policy, string password)
        {
            var sut = new PasswordValidator(new CharacterPositionValidationStrategy());

            var actual = sut.IsPasswordValid(policy, password);

            actual.Should().BeTrue();
        }

        [Theory]
        [InlineData("1-3 b", "cdefg")]
        [InlineData("2-9 c", "ccccccccc")]
        public void IsPasswordValid_ShouldReturnFalseForPositionValidationStrategy_WhenGivenInvalidPassword(string policy, string password)
        {
            var sut = new PasswordValidator(new CharacterPositionValidationStrategy());

            var actual = sut.IsPasswordValid(policy, password);

            actual.Should().BeFalse();
        }

        [Fact]
        public void FilterValidPasswords_ShouldReturnCorrectNumberOfItemsForPositionValidationStrategy_GivenProblemInput()
        {
            var input = File.ReadAllLines("Inputs\\day2.txt");
            var sut = new PasswordValidator(new CharacterPositionValidationStrategy());

            var actual = sut.FilterValidPasswords(input);

            actual.Length.Should().Be(588);
        }
    }
}