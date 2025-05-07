using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;


namespace Snake
{

    //------------  Главное окно, обрабатывающее весь игровой процесс. ---------------


    public partial class GameForm : Form // класс формы  — основное окно игры от Form.
    {
        private string LevelManagerDifficultyName()
        {
            return LevelManager.GetDifficultyName();
        }
        private Stopwatch stopwatch = new Stopwatch();  //таймер сверху

        private System.Windows.Forms.Timer gameTimer;   // таймер для движения змейки по тикам
        private const int cellSize = 20;      // размер ячейки
        private Snake snake;                  // объект змейки
        private Food food;                    // объект еды   
        private int elapsedSeconds = 0; // Время, прошедшее с начала игры (в секундах)

        //----

        //------------------------------------ Сама форма ---------------------------

        public GameForm()
        {
            InitializeComponent();

            this.Text = "Snake";
            this.Width = 600;
            this.Height = 400;
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            snake = new Snake(100, 100, cellSize); // создание объекта
            food = new Food(ClientSize.Width, ClientSize.Height, cellSize); // создаём еду в случайном месте внутри окна.
            Obstacle.Generate(ClientSize.Width, ClientSize.Height, cellSize); //создание стенок


            // старт

            stopwatch.Start();
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = LevelManager.GetStartInterval();
            gameTimer.Tick += GameTick;
            
            Invalidate();// Перерисовать форму, чтобы обновился экран


            ScoreManager.Reset();        // сброс очков
            LevelManager.Reset();        // сброс уровня
            GameSound.Init();            // загрузка звуков
            GameSound.PlayMusic();       // фоновая музыка

            //Запуск таймера и событий формы

            gameTimer.Start();// запускаем игровой таймер

            this.KeyDown += GameForm_KeyDown;
            this.Paint += GameForm_Paint;
        }

        //--------------------------------- Проверки и так далее ----------------------------------------

        private void GameTick(object? sender, EventArgs e) 
        {
            snake.Move();

            if (snake.IsSelfCollision() || IsWallCollision() || Obstacle.IsCollision(snake.Body[0]))
            {
                gameTimer.Stop();
                GameSound.StopMusic();
                GameSound.PlayLose();

                string name = PlayerInput.AskPlayerName();
                ScoreManager.SaveResult(name);
                HighScoreUI.Show();

                Close();
            }


            if (snake.Body[0] == food.Position) //Если голова змейки совпала с едой
            {
                snake.Grow();
                food.GenerateNew(ClientSize.Width, ClientSize.Height, cellSize);

                ScoreManager.AddPoint();
                LevelManager.UpdateLevel(gameTimer);
                GameSound.PlayEat();
            }

            if (snake.IsSelfCollision() || IsWallCollision())//Если змейка врезалась в себя или в стену
            {
                gameTimer.Stop();
                GameSound.StopMusic();
                GameSound.PlayLose();

                string name = PlayerInput.AskPlayerName();
                ScoreManager.SaveResult(name);
                HighScoreUI.Show();

                Close();
            }

            Invalidate(); // Нужно оставить, чтобы форма перерисовывалась
        }


        //----------------------- Проверка: не вышла ли змейка за пределы окна.------------------

        private bool IsWallCollision() 
        {
            Point head = snake.Body[0];
            return head.X < 0 || head.X >= ClientSize.Width || head.Y < 0 || head.Y >= ClientSize.Height;
        }

        // --------------------------------------- Кнопочки ---------------------------------------

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

        //--------------------------------- Дизайн так сказать ----------------------------------

        private void GameForm_Paint(object? sender, PaintEventArgs e){

            //Рисуем стенки
            Obstacle.Draw(e.Graphics, Brushes.Gray, cellSize);

            // Рисуем змейку (зелёным цветом)
            snake.Draw(e.Graphics, Brushes.Green);

            // Рисуем еду (красным цветом)
            food.Draw(e.Graphics, Brushes.Red);

            // Обновляем количество секунд с начала игры
            elapsedSeconds = (int)stopwatch.Elapsed.TotalSeconds;

            // Настраиваем шрифт и кисть для текста
            using Font font = new Font("Arial", 14);
            using Brush brush = Brushes.White;

            // Рисуем счёт
            string scoreText = $"Очки: {ScoreManager.Score}";
            e.Graphics.DrawString(scoreText, font, brush, new PointF(10, 10));

            // Рисуем время
            string timeText = $"Время: {elapsedSeconds} сек";
            e.Graphics.DrawString(timeText, font, brush, new PointF(10, 35));

            // Получаем цвет в зависимости от сложности
            Color difficultyColor = LevelManager.GetDifficultyColor();

            // Создаём кисть для сложности
            using Brush difficultyBrush = new SolidBrush(difficultyColor);

            // Рисуем сложность с цветом
            string difficultyText = $"Сложность: {LevelManager.GetDifficultyName()}";
            e.Graphics.DrawString(difficultyText, font, difficultyBrush, new PointF(10, 60));

          
        }

    }
}
