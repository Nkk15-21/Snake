using System;
using System.IO;
using System.Media;

namespace Snake
{
    /// <summary>
    /// Обрабатывает все звуковые эффекты игры.
    /// </summary>
    public static class GameSound
    {
        private static SoundPlayer? background;
        private static SoundPlayer? eat;
        private static SoundPlayer? lose;

        private static readonly string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources");

        public static void Init()
        {
            background = new SoundPlayer(Path.Combine(basePath, "background.wav"));
            eat = new SoundPlayer(Path.Combine(basePath, "bite.wav"));
            lose = new SoundPlayer(Path.Combine(basePath, "lose.wav"));
        }

        public static void PlayMusic() => background?.PlayLooping();
        public static void StopMusic() => background?.Stop();
        public static void PlayEat() => eat?.Play();
        public static void PlayLose() => lose?.Play();
    }
}
