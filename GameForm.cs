using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Snake
{
    /// <summary>
    /// Главное окно — сама игра.
    /// </summary>
    public partial class GameForm : Form
    {
        private Stopwatch stopwatch = new Stopwatch();                   // Таймер времени
        private Timer gameTimer;                                         // Таймер тиков змейки
        private const int cellSize = 20;                                 // Размер одной ячейки
        private Snake snake;                                             // Объект змейки
        private Food food;                                               // Объект еды
        private int elapsedSeconds = 0;                                  // Счётчик времени
        private List<Point> walls = new();                               // Список точек-стен

        public GameForm()
        {
            InitializeComponent();

            this.Text = "Snake";
            this.ClientSize = new Size(800, 600);                       // Размер игровой области
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            snake = new Snake(100, 100, cellSize);
            food = new Food(ClientSize.Width, ClientSize.Height, cellSize);

            GenerateWalls();                                            // Создаём стены

            stopwatch.Start();
            gameTimer = new Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTick;

            ScoreManager.Reset();
            LevelManager.Reset();
            GameSound.Init();
            GameSound.PlayMusic();

            gameTimer.Start();

            this.KeyDown += GameForm_KeyDown;
            this.Paint += GameForm_Paint;
        }

        /// <summary>
        /// Создаёт стены по краям игрового поля.
        /// </summary>
        private void GenerateWalls()
        {
            int width = ClientSize.Width;
            int height = ClientSize.Height;

            walls.Clear();

            // Горизонтальные стены (верхняя и нижняя)
            for (int x = 0; x < width; x += cellSize)
            {
                walls.Add(new Point(x, 0));                       // Верх
                walls.Add(new Point(x, height - cellSize));       // Низ
            }

            // Вертикальные стены (левая и правая)
            for (int y = 0; y < height; y += cellSize)
            {
                walls.Add(new Point(0, y));                       // Левая
                walls.Add(new Point(width - cellSize, y));        // Правая
            }
        }

        private void GameTick(object? sender, EventArgs e)
        {
            snake.Move();

            if (snake.Body[0] == food.Position)
            {
                snake.Grow();
                food.GenerateNew(ClientSize.Width, ClientSize.Height, cellSize);
                ScoreManager.AddPoint();
                LevelManager.UpdateLevel(gameTimer);
                GameSound.PlayEat();
            }

            if (snake.IsSelfCollision() || IsWallCollision())
            {
                gameTimer.Stop();
                GameSound.StopMusic();
                GameSound.PlayLose();

                string name = PlayerInput.AskPlayerName();
                ScoreManager.SaveResult(name);
                HighScoreUI.Show();

                Close();
            }

            Invalidate(); // Перерисовать форму
        }

        /// <summary>
        /// Проверка: врезалась ли змейка в стену.
        /// </summary>
        private bool IsWallCollision()
        {
            Point head = snake.Body[0];
            return walls.Contains(head);
        }

        private void GameForm_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (snake.CurrentDirection != Direction.Down)
                        snake.CurrentDirection = Direction.Up;
                    break;
                case Keys.Down:
                    if (snake.CurrentDirection != Direction.Up)
                        snake.CurrentDirection = Direction.Down;
                    break;
                case Keys.Left:
                    if (snake.CurrentDirection != Direction.Right)
                        snake.CurrentDirection = Direction.Left;
                    break;
                case Keys.Right:
                    if (snake.CurrentDirection != Direction.Left)
                        snake.CurrentDirection = Direction.Right;
                    break;
            }

            if (e.KeyCode == Keys.H)
                HighScoreUI.Show();
        }

        private void GameForm_Paint(object? sender, PaintEventArgs e)
        {
            // Обновляем прошедшее время
            elapsedSeconds = (int)stopwatch.Elapsed.TotalSeconds;

            // Шрифт и кисти
            using Font font = new("Arial", 14);
            using Brush white = Brushes.White;

            // Рисуем змейку и еду
            snake.Draw(e.Graphics, Brushes.Green);
            food.Draw(e.Graphics, Brushes.Red);

            // Рисуем стены
            foreach (var wall in walls)
                e.Graphics.FillRectangle(Brushes.Gray, wall.X, wall.Y, cellSize, cellSize);

            // Время
            e.Graphics.DrawString($"Время: {elapsedSeconds} сек", font, white, new PointF(10, 10));

            // Очки
            e.Graphics.DrawString($"Очки: {ScoreManager.Score}", font, white, new PointF(10, 35));

            // Сложность
            string difficulty = LevelManager.GetDifficultyName();
            Color diffColor = LevelManager.GetDifficultyColor();
            using Brush diffBrush = new SolidBrush(diffColor);
            e.Graphics.DrawString($"Сложность: {difficulty}", font, diffBrush, new PointF(10, 60));
        }
    }
}
