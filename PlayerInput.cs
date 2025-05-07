using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Snake
{
    /// <summary>
    /// Отвечает за запрос имени игрока с проверкой.
    /// </summary>
    public static class PlayerInput
    {
        public static string AskPlayerName()
        {
            string name = "";

            while (true)
            {
                // Запрашиваем имя
                name = Microsoft.VisualBasic.Interaction.InputBox(
                    "Введите ваше имя (не менее 3 символов):",
                    "Имя игрока",
                    "Игрок"
                );

                // Проверка на длину
                if (!string.IsNullOrWhiteSpace(name) && name.Length >= 3)
                    return name;

                // Если имя невалидно — показываем предупреждение
                MessageBox.Show("Имя должно быть не короче 3 символов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
