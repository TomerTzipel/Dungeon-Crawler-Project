using System;

namespace Utility
{
    public enum Direction
    {
        Up, Down, Left, Right, Error

    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Point MovePointInDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Y -= 1;
                    break;
                case Direction.Down:
                    Y += 1;
                    break;
                case Direction.Left:
                    X -= 1;
                    break;
                case Direction.Right:
                    X += 1;
                    break;
            }

            return this;
        }
        public Point MovePointInOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    MovePointInDirection(Direction.Down);
                    break;
                case Direction.Down:
                    MovePointInDirection(Direction.Up);
                    break;
                case Direction.Left:
                    MovePointInDirection(Direction.Right);
                    break;
                case Direction.Right:
                    MovePointInDirection(Direction.Left);
                    break;
            }

            return this;
        }
        public static float Distance(Point pointA,Point pointB)
        {
            float xDifference = pointB.X - pointA.X;
            float yDifference = pointB.Y - pointA.Y;
            float squaresSum = xDifference * xDifference + yDifference * yDifference;
            return MathF.Sqrt(squaresSum);
        }

        public static bool Equals(Point p1, Point p2)
        {
            return (p1.X == p2.X && p1.Y == p2.Y);
        }

        public override string ToString() 
        {
            return $"(X:{X},Y:{Y})";
        }
    }
}
