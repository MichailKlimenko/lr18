using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лр18
{
    // Класс для представления пользователя
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // Метод для вывода сообщения с приветствием пользователю по логину
        public void GreetUser()
        {
            MessageBox.Show("Добро пожаловать, " + Username + "!");
        }
    }

    // Класс для представления пиццы
    public class Pizza
    {
        public string Name { get; set; }
        public string Size { get; set; }

        // Метод для заполнения комбобокса именами пицц
        public static void FillPizzaComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear(); // Очистка комбобокса перед заполнением

            // Добавление пицц в комбобокс
            comboBox.Items.Add("Маргарита");
            comboBox.Items.Add("Пепперони");
            comboBox.Items.Add("Гавайская");
            comboBox.Items.Add("Вегетарианская");
            comboBox.Items.Add("Четыре сыра");
        }

        // Метод для заполнения комбобокса размерами пицц
        public static void FillPizzaSizeComboBox(ComboBox comboBox)
        {
            comboBox.Items.Clear(); // Очистка комбобокса перед заполнением

            // Добавление размеров в комбобокс
            comboBox.Items.Add("Маленькая");
            comboBox.Items.Add("Средняя");
            comboBox.Items.Add("Большая");
            comboBox.Items.Add("Огромная");
        }
    }
}
