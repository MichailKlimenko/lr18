using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; // Импорт пространства имен для работы с SQL Server
using System.Windows.Forms;

namespace лр18
{
    public partial class Form1 : Form
    {
        // Строка подключения к базе данных
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WolfGGO\Documents\pizza.mdf;Integrated Security=True;Connect Timeout=30";

        public Form1()
        {
            InitializeComponent(); // Инициализация компонентов формы
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(); // Создание экземпляра Form2
            form2.ShowDialog(); // Отображение Form2 как диалогового окна
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text; // Получение логина из текстового поля
            string password = textBox2.Text; // Получение пароля из текстового поля

            // Проверка наличия заполненных полей
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Проверка наличия пользователя в базе данных
                if (CheckUserInDatabase(username, password))
                {
                    Form3 form3 = new Form3(); // Создание экземпляра Form3
                    form3.Show(); // Отображение Form3
                }
                else
                {
                    MessageBox.Show("Пользователь с указанными данными не найден."); // Вывод сообщения об ошибке
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль пользователя."); // Вывод сообщения о необходимости заполнения полей
            }
        }

        // Метод для проверки наличия пользователя в базе данных
        private bool CheckUserInDatabase(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password"; // SQL-запрос для проверки наличия пользователя
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection); // Создание команды для выполнения SQL-запроса
                command.Parameters.AddWithValue("@Username", username); // Добавление параметра логина
                command.Parameters.AddWithValue("@Password", password); // Добавление параметра пароля

                try
                {
                    connection.Open(); // Открытие соединения с базой данных
                    count = (int)command.ExecuteScalar(); // Выполнение запроса и получение результата
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Вывод сообщения об ошибке, если произошла ошибка при выполнении запроса
                }
            }

            return count > 0; // Возвращение результата проверки (true, если пользователь найден; false, если нет)
        }
    }
}
