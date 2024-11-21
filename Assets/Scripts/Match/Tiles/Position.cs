using UnityEngine;

namespace Fighters.Match
{
    public struct Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position(Vector2 vector)
        {
            X = (int)vector.x;
            Y = (int)vector.y;
        }
        
        public int X { get; set; }
        public int Y { get; set; }
        
        public static Position Right => new Position(1, 0);
        public static Position Left => new Position(-1, 0);
        public static Position Up => new Position(0, 1);
        public static Position Down => new Position(0, -1);
        public static Position Zero => new Position(0, 0);

        public static Position operator *(Position position, int number)
        {
            return new Position(position.X * number, position.Y * number);
        }
    }
}