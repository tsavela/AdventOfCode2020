using System;
using System.Linq;

namespace Implementation
{
    public class CustomsFormCalculator
    {
        public string CalculateAnswersSomeoneAnsweredYesOn(string[] input)
        {
            return string.Concat(input.SelectMany(i => i.ToArray()).Distinct().OrderBy(i => i));
        }

        public string CalculateAnswersEveryoneAnsweredYesOn(string[] input)
        {
            var possibleAnswers = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            return string.Concat(possibleAnswers.Where(answer => input.Where(input => input.Length > 0).All(input => input.ToLower().Contains(answer))));
        }
    }
}