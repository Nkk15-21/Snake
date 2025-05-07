using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    /// <summary>
    /// Представляет еду: случайная позиция + отрисовка.
    /// </summary>
    public class Food
    {
        public Point Position { get; private set; }
        private Random random = new Random();

        public Food(int maxWidth, int maxHeight, int cellSize)
        {
            GenerateNew(maxWidth, maxHeight, cellSize);
        }

        public void GenerateNew(int maxWidth, int maxHeight, int cellSize)
        {
            int x = random.Next(0, maxWidth / cellSize) * cellSize;
            int y = random.Next(0, maxHeight / cellSize) * cellSize;
            Position = new Point(x, y);
        }

        public void Draw(Graphics g, Brush brush)
        {
            g.FillRectangle(brush, Position.X, Position.Y, 20, 20);
        }
    }
}
