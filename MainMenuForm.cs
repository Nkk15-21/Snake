using System;
using System.Drawing;
using System.Windows.Forms;
using Snake;

namespace Snake
{

    // Главное меню игры: выбор сложности, запуск, выход, таблица рекордов

    public partial class MainMenuForm : Form
    {
        private Button btnStart;
        private Button btnScore;
        private Button btnExit;
        private ComboBox difficultyBox;
        private Label lblDifficulty;

        public MainMenuForm()
        {
            // Настройки формы
            this.Text = "Меню";
            this.Size = new Size(300, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Выпадающий список выбора сложности
            difficultyBox = new ComboBox
            {
                Location = new Point(50, 100),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            difficultyBox.Items.AddRange(new[] { "Лёгкий", "Средний", "Сложный" });
            difficultyBox.SelectedIndex = 1; // По умолчанию "Средний"



            // ------------------ Копки ---------------------

            // Кнопка "Старт игры"
            btnStart = new Button
            {
                Text = "Старт игры",
                Size = new Size(200, 30),
                Location = new Point(50, 30)
            };
            btnStart.Click += BtnStart_Click;

            // Метка для выбора сложности
            lblDifficulty = new Label
            {
                Text = "Сложность:",
                Location = new Point(50, 75),
                Size = new Size(200, 20)
            };

            // Кнопка "Рекорды"
            btnScore = new Button
            {
                Text = "Таблица рекордов",
                Size = new Size(200, 30),
                Location = new Point(50, 140)
            };
            btnScore.Click += (s, e) => HighScoreUI.Show();

            // Кнопка "Выход"
            btnExit = new Button
            {
                Text = "Выход",
                Size = new Size(200, 30),
                Location = new Point(50, 180)
            };
            btnExit.Click += (s, e) => Application.Exit();

            // Добавляем элементы на форму
            Controls.Add(btnStart);
            Controls.Add(lblDifficulty);
            Controls.Add(difficultyBox);
            Controls.Add(btnScore);
            Controls.Add(btnExit);
        }

        //------------------------- Обработка кнопки "Старт игры"--------------------

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            string? selected = difficultyBox.SelectedItem as string;

            if (selected == null)
            {
                MessageBox.Show("Пожалуйста, выберите уровень сложности.");
                return;
            }

            switch (selected)
            {
                case "Лёгкий":
                    LevelManager.SetDifficulty(LevelDifficulty.Easy);
                    break;
                case "Средний":
                    LevelManager.SetDifficulty(LevelDifficulty.Medium);
                    break;
                case "Сложный":
                    LevelManager.SetDifficulty(LevelDifficulty.Hard);
                    break;
                default:
                    MessageBox.Show("Неизвестная сложность.");
                    return;
            }

            GameForm game = new GameForm();
            this.Hide();
            game.ShowDialog();
            this.Show();
        }
    }
}
