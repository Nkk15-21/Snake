using System;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    /// Точка входа в программу.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameForm());
        }
    }
}
