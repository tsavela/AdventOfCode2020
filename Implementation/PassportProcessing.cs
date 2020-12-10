using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Implementation
{
    public class PassportProcessing
    {
        private static readonly (string Key, Func<string, bool> Validator)[] RequiredFields = new (string Key, Func<string, bool> Validator)[]
        {
            ("byr", s => ValidateIntRange(s, 1920, 2002)),
            ("iyr", s => ValidateIntRange(s, 2010, 2020)),
            ("eyr", s => ValidateIntRange(s, 2020, 2030)),
            ("hgt", s =>
            {
                if (s.Length < 4) return false;
                var suffix = s.Substring(s.Length - 2, 2);
                var value = s.Substring(0, s.Length - 2);
                return suffix switch
                {
                    "cm" => ValidateIntRange(value, 150, 193),
                    "in" => ValidateIntRange(value, 59, 76),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }),
            ("hcl", s =>
            {
                var regex = new Regex("^#[0-9a-f]{6}$");
                return regex.IsMatch(s);
            }),
            ("ecl", s =>
            {
                return new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(s);
            }),
            ("pid", s =>
            {
                var regex = new Regex("^[0-9]{9}$");
                return regex.IsMatch(s);
            })
        };

        private static bool ValidateIntRange(string s, int min, int max)
        {
            if (!int.TryParse(s, out var value)) return false;
            return value >= min && value <= max;
        }

        public bool ValidatePassport(PassportData[] passportData)
        {
            return RequiredFields.All(field => passportData.Any(pd => pd.Key == field.Key && field.Validator(pd.Value)));
        }

        public PassportData[][] ParseInputData(string input)
        {
            var blocks = input.Split("\n\n");
            return blocks.Select(block =>
            {
                var keyAndValues = block.Split(' ', '\n');
                return keyAndValues.Where(kv => kv != "").Select(kv =>
                {
                    var keyAndValue = kv.Split(':');
                    return new PassportData(keyAndValue[0], keyAndValue[1]);
                }).ToArray();
            }).ToArray();
        }
    }

    public record PassportData(string Key, string Value);
}