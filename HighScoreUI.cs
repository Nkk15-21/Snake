using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
    


namespace Snake
{

    // Отображает список лучших игроков в отдельном окне.

    public static class HighScoreUI
    {
        public static void Show()
        {
            List<(string name, int score)> results = ScoreManager.LoadResults();

            Form form = new Form
            {
                Text = "Лучшие игроки",
                Size = new Size(300, 400),
                StartPosition = FormStartPosition.CenterScreen
            };

            ListBox listBox = new ListBox
            {
                Dock = DockStyle.Fill, //расширяется на всю форму
                Font = new Font("Consolas", 12)
            };

            // Используем деструктуризацию (name, score) для читаемости

            foreach ((string name, int score) in results)
            {
                string line = $"{name.PadRight(15)}  {score}"; //PadRight(15) — выравнивает имя по ширине (15 символов)
                listBox.Items.Add(line);
            }

            form.Controls.Add(listBox);
            form.ShowDialog();
        }
    }

}

