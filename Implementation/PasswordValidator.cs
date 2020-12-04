using System;
using System.Linq;

namespace Implementation
{
    public class PasswordValidator
    {
        private readonly IPasswordValidationStrategy _validationStrategy;

        public PasswordValidator(IPasswordValidationStrategy validationStrategy)
        {
            _validationStrategy = validationStrategy;
        }

        public bool IsPasswordValid(string policy, string password)
        {
            return _validationStrategy.IsPasswordValid(policy, password);
        }

        public string[] FilterValidPasswords(string[] input)
        {
            return input.Where(i =>
            {
                var parts = i.Split(": ");
                return IsPasswordValid(parts[0], parts[1]);
            }).ToArray();
        }
    }

    public interface IPasswordValidationStrategy
    {
        bool IsPasswordValid(string policy, string password);
    }

    public class CharacterPositionValidationStrategy : IPasswordValidationStrategy
    {
        public bool IsPasswordValid(string policy, string password)
        {
            var (character, firstPosition, secondPosition) = ParsePolicy(policy);
            var characterIsAtFirstPosition = password[firstPosition - 1] == character;
            var characterIsAtSecondPosition = password[secondPosition - 1] == character;

            return characterIsAtFirstPosition && !characterIsAtSecondPosition
                   || !characterIsAtFirstPosition && characterIsAtSecondPosition;
        }

        private (char Character, int FirstPosition, int SecondPosition) ParsePolicy(string policy)
        {
            var parts = policy.Split(new[] {'-', ' '});
            return (parts[2].Single(), int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }

    public class MaxMinCharacterCountValidationStrategy : IPasswordValidationStrategy
    {
        public bool IsPasswordValid(string policy, string password)
        {
            var (character, minimumCount, maximumCount) = ParsePolicy(policy);

            var existingCharacterCount = password.Count(c => c == character);

            return existingCharacterCount >= minimumCount && existingCharacterCount <= maximumCount;

        }

        private (char Character, int Minimum, int Maximum) ParsePolicy(string policy)
        {
            var parts = policy.Split(new[] {'-', ' '});
            return (parts[2].Single(), int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}