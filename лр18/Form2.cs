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
    public partial class Form2 : Form
    {
        // Строка подключения к базе данных
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WolfGGO\Documents\pizza.mdf;Integrated Security=True;Connect Timeout=30";

        public Form2()
        {
            InitializeComponent(); // Инициализация компонентов формы
        }

        // Обработчик нажатия кнопки "button1"
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text; // Получение логина пользователя из текстового поля
            string password = textBox2.Text; // Получение пароля пользователя из текстового поля

            // Проверка наличия введенного логина и пароля
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                AddUserToDatabase(username, password); // Вызов метода для добавления пользователя в базу данных
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль пользователя."); // Вывод сообщения о необходимости ввода логина и пароля
            }

            this.Close(); // Закрытие формы
        }

        // Метод для добавления пользователя в базу данных
        private void AddUserToDatabase(string username, string password)
        {
            string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)"; // SQL-запрос для добавления пользователя

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection); // Создание команды для выполнения SQL-запроса
                command.Parameters.AddWithValue("@Username", username); // Добавление параметра с логином пользователя
                command.Parameters.AddWithValue("@Password", password); // Добавление параметра с паролем пользователя

                try
                {
                    connection.Open(); // Открытие соединения с базой данных
                    int rowsAffected = command.ExecuteNonQuery(); // Выполнение запроса и получение числа затронутых строк

                    // Проверка успешности выполнения запроса и вывод соответствующего сообщения
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Пользователь успешно добавлен.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении пользователя.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Вывод сообщения об ошибке, если произошла ошибка при выполнении запроса
                }
            }
        }
    }
}
