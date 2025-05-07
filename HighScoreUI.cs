using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Collections.Generic;


namespace Snake
{
    /// <summary>
    /// Отображает список лучших игроков в отдельном окне.
    /// </summary>
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
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 12)
            };

            // Важно: деструктуризация (name, score)
            foreach ((string name, int score) in results)
            {
                string line = $"{name.PadRight(15)}  {score}";
                listBox.Items.Add(line);
            }

            form.Controls.Add(listBox);
            form.ShowDialog();
        }
    }

}

