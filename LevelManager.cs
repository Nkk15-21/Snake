using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Snake
{
    /// <summary>
    /// Менеджер уровней — ускоряет игру каждые 5 очков.
    /// </summary>
    public static class LevelManager
    {
        public static int CurrentLevel { get; private set; } = 1;
        private const int MinInterval = 40;

        public static void Reset()
        {
            CurrentLevel = 1;
        }

        public static void UpdateLevel(System.Windows.Forms.Timer timer)
        {
            int score = ScoreManager.Score;
            int newLevel = (score / 5) + 1;

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
