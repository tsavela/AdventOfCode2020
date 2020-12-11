using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Implementation
{
    public class Seat
    {
        private static readonly Regex SeatDefinitionRegex = new Regex("^[FB]{7}[LR]{3}$");

        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Id { get; private set; }

        public Seat(string seatDefinition)
        {
            if (!SeatDefinitionRegex.IsMatch(seatDefinition))
                throw new ArgumentException("Invalid seat definition", nameof(seatDefinition));

            Row = CalculateRow(seatDefinition.Substring(0, 7), Enumerable.Range(0, 128).ToArray());
            Column = CalculateColumn(seatDefinition.Substring(7), Enumerable.Range(0, 8).ToArray());
            Id = CalculateId(Row, Column);
        }

        private int CalculateId(in int row, in int column)
        {
            return row * 8 + column;
        }

        private int CalculateColumn(string columnDefinition, int[] availableColumns)
        {
            return CalculateInternal(columnDefinition, availableColumns, 'L');
        }

        private int CalculateRow(string rowDefinition, int[] availableRows)
        {
            return CalculateInternal(rowDefinition, availableRows, 'F');
        }

        private int CalculateInternal(string definition, int[] available, char firstPartSelector)
        {
            var partition = GetPartition(available, definition[0]);
            if (definition.Length == 1) return partition.Single();
            return CalculateInternal(definition.Substring(1), partition, firstPartSelector);

            int[] GetPartition(int[] rows, char selector)
            {
                var startIndex = selector == firstPartSelector ? 0 : rows.Length / 2;
                return rows.Skip(startIndex).Take(rows.Length / 2).ToArray();
            }
        }
    }
}