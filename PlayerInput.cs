using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    /// <summary>
    /// Класс отвечает за ввод имени игрока с проверкой (не менее 3 символов).
    /// </summary>
    public static class PlayerInput
    {
        /// <summary>
        /// Показывает окно ввода имени игрока и проверяет его.
        /// </summary>
        /// <returns>Имя игрока (строка не короче 3 символов).</returns>
        public static string AskPlayerName()
        {
            string name = ""; // переменная для хранения имени

            // Цикл продолжается до тех пор, пока не введено валидное имя
            while (true)
            {
                // Показываем окно ввода
                name = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите ваше имя (не менее 3 символов):", // текст в окне
                    "Имя игрока", // заголовок окна
                    "Игрок" // значение по умолчанию
                );

                // Проверка: имя не пустое и длина не менее 3 символов
                if (!string.IsNullOrWhiteSpace(name) && name.Length >= 3)
                {
                    return name; // имя корректное, возвращаем его
                }
                else
                {
                    // Если имя некорректное — выводим предупреждение
                    MessageBox.Show("Имя должно содержать не менее 3 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
