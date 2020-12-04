namespace Implementation
{
    public class TobogganTrajectory
    {
        private const char Tree = '#';

        public long CalculateNumberOfTrees(string[] map, (int Right, int Down) trajectory)
        {
            var currentMapRow = 0;
            var currentMapColumn = 0;
            var trees = 0L;
            var mapWidth = map[0].Length;

            while (currentMapRow < map.Length)
            {
                if (map[currentMapRow][currentMapColumn % mapWidth] == Tree)
                    trees += 1;

                currentMapColumn += trajectory.Right;
                currentMapRow += trajectory.Down;
            }

            return trees;
        }
    }
}