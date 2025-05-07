using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    /// <summary>
    /// Класс Snake — логика движения, роста и столкновений змейки.
    /// </summary>
    public class Snake
    {
        public List<Point> Body { get; private set; }
        public Direction CurrentDirection { get; set; }

        private int cellSize;

        public Snake(int startX, int startY, int cellSize)
        {
            this.cellSize = cellSize;

            Body = new List<Point>
            {
                new Point(startX, startY),
                new Point(startX - cellSize, startY),
                new Point(startX - 2 * cellSize, startY)
            };

            CurrentDirection = Direction.Right;
        }

        public void Move()
        {
            Point head = Body[0];
            Point newHead = head;

            switch (CurrentDirection)
            {
                case Direction.Up: newHead.Y -= cellSize; break;
                case Direction.Down: newHead.Y += cellSize; break;
                case Direction.Left: newHead.X -= cellSize; break;
                case Direction.Right: newHead.X += cellSize; break;
            }

            Body.Insert(0, newHead);
            Body.RemoveAt(Body.Count - 1);
        }

        public void Grow()
        {
            Point tail = Body[Body.Count - 1];
            Body.Add(tail);
        }

        public bool IsSelfCollision()
        {
            for (int i = 1; i < Body.Count; i++)
            {
                if (Body[0] == Body[i])
                    return true;
            }
            return false;
        }

        public void Draw(Graphics g, Brush brush)
        {
            foreach (Point part in Body)
                g.FillRectangle(brush, part.X, part.Y, cellSize, cellSize);
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
