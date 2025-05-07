using System;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Включаем визуальные стили для формы
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запускаем главное окно игры
            Application.Run(new MainMenuForm());

        }
    }
}
