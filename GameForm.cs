using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    /// Главное окно, обрабатывающее весь игровой процесс.
    /// </summary>
    public partial class GameForm : Form // класс формы
    {


        private System.Windows.Forms.Timer gameTimer;   // таймер для движения змейки
        private const int cellSize = 20;      // размер ячейки
        private Snake snake;                  // объект змейки
        private Food food;                    // объект еды

        /// <summary>
        /// Время, прошедшее с начала игры (в секундах)
        /// </summary>
        private int elapsedSeconds = 0;

        public GameForm()
        {
            InitializeComponent();

            this.Text = "Snake";
            this.Width = 600;
            this.Height = 400;
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            snake = new Snake(100, 100, cellSize);
            food = new Food(ClientSize.Width, ClientSize.Height, cellSize);

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTick;
            elapsedSeconds++;       // Увеличиваем время
            Invalidate();           // Перерисовать форму, чтобы обновился экран


            ScoreManager.Reset();        // сброс очков
            LevelManager.Reset();        // сброс уровня
            GameSound.Init();            // загрузка звуков
            GameSound.PlayMusic();       // фоновая музыка

            gameTimer.Start();

            this.KeyDown += GameForm_KeyDown;
            this.Paint += GameForm_Paint;
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

            Invalidate(); // перерисовать экран
        }



        private bool IsWallCollision()
        {
            Point head = snake.Body[0];
            return head.X < 0 || head.X >= ClientSize.Width || head.Y < 0 || head.Y >= ClientSize.Height;
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
            {
                HighScoreUI.Show();
            }
        }

        private void GameForm_Paint(object? sender, PaintEventArgs e)
        {
            // Рисуем змейку и еду
            snake.Draw(e.Graphics, Brushes.Green);
            food.Draw(e.Graphics, Brushes.Red);

            // === Рисуем время игры ===
            string timeText = $"Время: {elapsedSeconds} сек";
            e.Graphics.DrawString(timeText, new Font("Arial", 14), Brushes.White, new PointF(10, 10));

            // === Рисуем текущий счёт ===
            string scoreText = $"Очки: {ScoreManager.Score}";
            e.Graphics.DrawString(scoreText, new Font("Arial", 14), Brushes.White, new PointF(10, 35));
        }

    }
}
