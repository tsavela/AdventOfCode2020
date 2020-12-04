using System;
using System.Linq;

namespace Implementation
{
    public class ExpenseReportCalculator
    {
        public (int first, int second)? FindPairWithSum(int[] values, int sum)
        {
            foreach (var value in values)
            {
                var remaining = sum - value;
                if (values.Contains(remaining)) return (value, remaining);
            }

            return null;
        }

        public (int first, int second, int third)? FindTripletWithSum(int[] values, int sum)
        {
            foreach (var value in values)
            {
                var remaining = sum - value;
                var pair = FindPairWithSum(values.Except(new[] {value}).ToArray(), remaining);
                if (pair != null) return (value, pair.Value.first, pair.Value.second);
            }

            return null;
        }
    }
}