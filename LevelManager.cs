using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing; // ← должен быть здесь, не внутри namespace

namespace Snake
{

    // Менеджер уровней — регулирует скорость игры в зависимости от очков и сложности.

    // Уровни сложности
    public enum LevelDifficulty{
        Easy,
        Medium,
        Hard
    }

    public static class LevelManager
    {

        public static int GetStartInterval()
        {
            return MinInterval;
        }

        public static int CurrentLevel { get; private set; } = 1;
        private static int MinInterval;
        private static int ScorePerLevel;

        // Текущая выбранная сложность
        private static LevelDifficulty difficulty = LevelDifficulty.Medium;

        /// <summary>
        /// Устанавливает сложность, выбранную игроком
        /// </summary>
        public static void SetDifficulty(LevelDifficulty level)
        {
            difficulty = level;

            switch (difficulty)
            {
                case LevelDifficulty.Easy:
                    MinInterval = 100;
                    ScorePerLevel = 10;
                    break;
                case LevelDifficulty.Medium:
                    MinInterval = 60;
                    ScorePerLevel = 5;
                    break;
                case LevelDifficulty.Hard:
                    MinInterval = 40;
                    ScorePerLevel = 3;
                    break;
            }

            Console.WriteLine($"[DEBUG] Установлена сложность: {difficulty}, ScorePerLevel = {ScorePerLevel}, MinInterval = {MinInterval}");
        }



        // Возвращает текущую сложность в виде строки

        public static string GetDifficultyName()
        {
            return difficulty switch
            {
                LevelDifficulty.Easy => "Лёгкий",
                LevelDifficulty.Medium => "Средний",
                LevelDifficulty.Hard => "Сложный",
                _ => "Неизвестно"
            };
        }


        // Цвет сложности (для отображения)

        public static Color GetDifficultyColor()
        {
            return difficulty switch
            {
                LevelDifficulty.Easy => Color.LightGreen,
                LevelDifficulty.Medium => Color.Gold,
                LevelDifficulty.Hard => Color.IndianRed,
                _ => Color.White
            };
        }


        // Возвращает количество очков за еду в зависимости от сложности

        public static int GetPointsPerFood()
        {
            return difficulty switch
            {
                LevelDifficulty.Easy => 1,
                LevelDifficulty.Medium => 2,
                LevelDifficulty.Hard => 3,
                _ => 1
            };
        }


        // Сброс уровня

        public static void Reset()
        {
            CurrentLevel = 1;
        }


        // Обновление уровня и интервала таймера в зависимости от очков

        public static void UpdateLevel(System.Windows.Forms.Timer timer)

        {
            int score = ScoreManager.Score;
            int newLevel = (score / ScorePerLevel) + 1;

            if (newLevel > CurrentLevel)
            {
                CurrentLevel = newLevel;

                int newInterval = timer.Interval - 10;
                if (newInterval < MinInterval)
                    newInterval = MinInterval;

                timer.Interval = newInterval;
            }
        }

    }
}
