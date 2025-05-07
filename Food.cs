using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake // объединяет все классы игры в одну логическую группу.
{
   
//------------- Представляет еду: случайная позиция + отрисовка. ---------
    

    public class Food // описывает поведение еды в игре: где она находится, как сгенерировать новое положение, как нарисовать на экране
    {
        public Point Position { get; private set; } // Position - хранит координаты еды (в пикселях)
        private Random random = new Random(); // генератор  

        //---- принимает максимальные размеры поля и размер клетки

        public Food(int maxWidth, int maxHeight, int cellSize) 
        {
            GenerateNew(maxWidth, maxHeight, cellSize);
        }

        //---- Метод для обновления позиции еды:

        public void GenerateNew(int maxWidth, int maxHeight, int cellSize) 
        {
            int x = random.Next(0, maxWidth / cellSize) * cellSize;
            int y = random.Next(0, maxHeight / cellSize) * cellSize;
            Position = new Point(x, y);
        }

        //---- принимает Graphics g (куда рисовать)

        public void Draw(Graphics g, Brush brush, int cellSize)
        {
            g.FillRectangle(brush, Position.X, Position.Y, cellSize, cellSize); //Рисует квадрат еды
        }

    }
}
