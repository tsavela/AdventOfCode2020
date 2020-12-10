using System.IO;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Implementation;
using Xunit;

namespace Tests
{
    public class Day4Tests
    {
        [Fact]
        public void ValidatePassport_ShouldReturnTrue_WhenGivenValidPassport()
        {
            var sut = new PassportProcessing();

            var actual = sut.ValidatePassport(sut.ParseInputData("pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980 hcl:#623a2f")[0]);

            actual.Should().BeTrue();
        }

        [Fact]
        public void ValidatePassport_ShouldReturnFalse_WhenGivenPassportWithMissingRequiredData()
        {
            var sut = new PassportProcessing();

            var actual = sut.ValidatePassport(sut.ParseInputData("pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980")[0]);

            actual.Should().BeFalse();
        }

        [Fact]
        public void ParseInputData_ShouldReturnCorrectData_WhenGivenOnePassport()
        {
            var sut = new PassportProcessing();

            var actual = sut.ParseInputData("ecl:gry pid:860033327 eyr:2020 hcl:#fffffd\nbyr:1937 iyr:2017 cid:147 hgt:183cm");

            actual.Length.Should().Be(1);
            actual.First().Length.Should().Be(8);
        }

        [Fact]
        public void ValidatePassport_ShouldReturnNumberOfValidPassports_WhenIteratedOverInputData()
        {
            var input = File.ReadAllText("Inputs\\day4.txt");
            var sut = new PassportProcessing();
            var passportsData = sut.ParseInputData(input);

            var count = passportsData.Count(pd => sut.ValidatePassport(pd));

            count.Should().Be(101);
        }

        [Theory]
        [InlineData("eyr:1972 cid:100 hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926")]
        [InlineData("iyr:2019 hcl:#602927 eyr:1967 hgt:170cm ecl:grn pid:012533040 byr:1946")]
        [InlineData("hcl:dab227 iyr:2012 ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277")]
        [InlineData("hgt:59cm ecl:zzz eyr:2038 hcl:74454a iyr:2023 pid:3556412378 byr:2007")]
        public void ValidatePassport_ShouldReturnFalse_WhenGivenDataValuesOutOfRange(string input)
        {
            var sut = new PassportProcessing();
            var passportData = sut.ParseInputData(input)[0];

            var actual = sut.ValidatePassport(passportData);

            actual.Should().BeFalse();
        }

        private PassportData[] GeneratePassportData(params string[] keys)
        {
            var fixture = new Fixture();
            return keys.Select(s => new PassportData(s,fixture.Create<string>())).ToArray();
        }
    }
}