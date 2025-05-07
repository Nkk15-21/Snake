using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    /// Управляет очками и сохранением результатов в файл.
    /// </summary>
    public static class ScoreManager
    {
        private static readonly string FilePath = "Nimed.txt";
        public static int Score { get; private set; } = 0;

        public static void AddPoint()
        {
            Score++;
        }

        public static void Reset()
        {
            Score = 0;
        }

        public static void SaveResult(string playerName)
        {
            try
            {
                string entry = $"{playerName}:{Score}";
                File.AppendAllLines(FilePath, new[] { entry });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        public static List<(string name, int score)> LoadResults()
        {
            var results = new List<(string, int)>();

            if (!File.Exists(FilePath))
                return results;

            foreach (var line in File.ReadAllLines(FilePath))
            {
                var parts = line.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out int s))
                    results.Add((parts[0], s));
            }

            MessageBox.Show("Загружено строк: " + results.Count); // <-- Временный отладочный вывод

            return results.OrderByDescending(r => r.Item2).ToList();
        }

    }
}
