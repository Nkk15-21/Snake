using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    /// Управляет подсчётом очков, сохранением и загрузкой результатов.
    /// </summary>
    public static class ScoreManager
    {
        private static readonly string FilePath = "Nimed.txt";
        public static int Score { get; private set; } = 0;

        /// <summary>
        /// Увеличивает счёт на 1 очко.
        /// </summary>
        public static void AddPoint()
        {
            Score++;
        }

        /// <summary>
        /// Сохраняет имя и результат в файл.
        /// </summary>
        public static void SaveResult(string playerName)
        {
            try
            {
                string entry = $"{playerName}:{Score}";
                File.AppendAllLines(FilePath, new[] { entry });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения результатов: " + ex.Message);
            }
        }

        /// <summary>
        /// Загружает результаты из файла и сортирует по убыванию.
        /// </summary>
        public static List<(string name, int score)> LoadResults()
        {
            var results = new List<(string, int)>();

            if (!File.Exists(FilePath))
                return results;

            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                {
                    results.Add((parts[0], score));
                }
            }

            return results.OrderByDescending(r => r.score).ToList();
        }

        /// <summary>
        /// Показывает список лучших игроков в отдельном окне.
        /// </summary>
        public static void ShowHighScores()
        {
            var results = LoadResults();
            string text = "Лучшие результаты:\n\n";

            foreach (var result in results)
            {
                text += $"{result.name} — {result.score}\n";
            }

            MessageBox.Show(text, "Рекорды");
        }

        /// <summary>
        /// Сбросить текущий счёт.
        /// </summary>
        public static void Reset()
        {
            Score = 0;
        }
    }
}
