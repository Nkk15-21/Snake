using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    /// <summary>
    /// Управляет препятствиями на карте
    /// </summary>
    public static class Obstacle
    {
        public static List<Point> Walls { get; private set; } = new List<Point>();

        // Генерация препятствий
        public static void Generate(int width, int height, int cellSize)
        {
            Walls.Clear();

            // Пример: рамка по краям (можно убрать, если не нужна)
            
            for (int x = 0; x < width; x += cellSize)
            {
                Walls.Add(new Point(x, 0));
                Walls.Add(new Point(x, height - cellSize));
            }
            for (int y = 0; y < height; y += cellSize)
            {
                Walls.Add(new Point(0, y));
                Walls.Add(new Point(width - cellSize, y));
            }
            

            // Пример: прямоугольник по центру
            int startX = width / 3;
            int startY = height / 3;

            for (int x = 0; x < 6; x++)
            {
                Walls.Add(new Point(startX + x * cellSize, startY));
                Walls.Add(new Point(startX + x * cellSize, startY + 3 * cellSize));
            }
            for (int y = 1; y < 3; y++)
            {
                Walls.Add(new Point(startX, startY + y * cellSize));
                Walls.Add(new Point(startX + 5 * cellSize, startY + y * cellSize));
            }
        }

        public static void Draw(Graphics g, Brush brush, int cellSize)
        {
            foreach (var wall in Walls)
            {
                g.FillRectangle(brush, wall.X, wall.Y, cellSize, cellSize);
            }
        }

        public static bool IsCollision(Point p)
        {
            return Walls.Contains(p);
        }
    }
}
