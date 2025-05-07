using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    /// Главное окно игры. Управляет отрисовкой, движением, логикой игры.
    /// </summary>
    public partial class GameForm : Form
    {
        private Timer gameTimer;
        private const int cellSize = 20;

        private Snake snake;
        private Food food;

        public GameForm()
        {
            InitializeComponent();

            this.Text = "Snake";
            this.Width = 600;
            this.Height = 400;
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            snake = new Snake(100, 100, cellSize);
            food = new Food(this.ClientSize.Width, this.ClientSize.Height, cellSize);

            gameTimer = new Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTick;
            gameTimer.Start();

            this.KeyDown += GameForm_KeyDown;
            this.Paint += GameForm_Paint;
        }

        private void GameTick(object sender, EventArgs e)
        {
            snake.Move();

            if (snake.Body[0] == food.Position)
            {
                snake.Grow();
                food.GenerateNew(this.ClientSize.Width, this.ClientSize.Height, cellSize);
            }

            if (snake.IsSelfCollision() || IsWallCollision())
            {
                gameTimer.Stop();
                MessageBox.Show("Вы проиграли!");
                this.Close();
            }

            this.Invalidate();
        }

        private bool IsWallCollision()
        {
            Point head = snake.Body[0];
            return head.X < 0 || head.X >= this.ClientSize.Width ||
                   head.Y < 0 || head.Y >= this.ClientSize.Height;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (snake.CurrentDirection != Direction.Down)
                        snake.CurrentDirection = Direction.Up;
                    break;
                case Keys.Down:
                    if (snake
