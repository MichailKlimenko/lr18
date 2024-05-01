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
    public partial class Form4 : Form
    {
        // Строка подключения к базе данных
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WolfGGO\Documents\pizza.mdf;Integrated Security=True;Connect Timeout=30";

        public Form4()
        {
            InitializeComponent(); // Инициализация компонентов формы
            FillDistrictsComboBoxes(); // Заполнение комбобоксов данными из базы данных
        }

        // Метод для заполнения комбобоксов данными из базы данных
        private void FillDistrictsComboBoxes()
        {
            string query = "SELECT DistrictName FROM Districts"; // SQL-запрос для получения названий районов из таблицы Districts

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection); // Создание команды для выполнения SQL-запроса

                try
                {
                    connection.Open(); // Открытие соединения с базой данных
                    SqlDataReader reader = command.ExecuteReader(); // Выполнение запроса и получение данных

                    // Чтение данных и заполнение комбобоксов
                    while (reader.Read())
                    {
                        string districtName = reader["DistrictName"].ToString();
                        comboBox1.Items.Add(districtName);
                        comboBox2.Items.Add(districtName);
                    }

                    reader.Close(); // Закрытие ридера
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Вывод сообщения об ошибке, если произошла ошибка при выполнении запроса
                }
            }
        }

        // Метод для обработки нажатия кнопки добавления нового района
        private void button1_Click(object sender, EventArgs e)
        {
            string districtName = textBox1.Text; // Получение названия района из текстового поля

            if (!string.IsNullOrEmpty(districtName))
            {
                AddDistrictToDatabase(districtName); // Добавление района в базу данных
                FillDistrictsComboBoxes(); // Обновление данных в комбобоксах после добавления нового района
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите название района."); // Вывод сообщения о необходимости ввода названия района
            }
        }

        // Метод для обработки нажатия кнопки добавления связи между районами
        private void button2_Click(object sender, EventArgs e)
        {
            string startDistrict = comboBox1.Text; // Получение названия начального района из комбобокса
            string endDistrict = comboBox2.Text; // Получение названия конечного района из комбобокса
            int distance;

            // Проверка наличия выбранных районов и корректности введенного расстояния
            if (!string.IsNullOrEmpty(startDistrict) && !string.IsNullOrEmpty(endDistrict) && int.TryParse(textBox2.Text, out distance))
            {
                AddConnectionToDatabase(startDistrict, endDistrict, distance); // Добавление связи между районами в базу данных
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите начальный и конечный районы и введите корректное расстояние."); // Вывод сообщения об ошибке
            }
        }

        // Метод для добавления нового района в базу данных
        private void AddDistrictToDatabase(string districtName)
        {
            string query = "INSERT INTO Districts (DistrictName) VALUES (@DistrictName)"; // SQL-запрос для добавления нового района

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection); // Создание команды для выполнения SQL-запроса
                command.Parameters.AddWithValue("@DistrictName", districtName); // Добавление параметра с названием района

                try
                {
                    connection.Open(); // Открытие соединения с базой данных
                    int rowsAffected = command.ExecuteNonQuery(); // Выполнение запроса и получение числа затронутых строк

                    // Проверка успешности выполнения запроса и вывод соответствующего сообщения
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Район успешно добавлен.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении района.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Вывод сообщения об ошибке, если произошла ошибка при выполнении запроса
                }
            }
        }

        // Метод для добавления связи между районами в базу данных
        private void AddConnectionToDatabase(string startDistrict, string endDistrict, int distance)
        {
            string query = "INSERT INTO CityMap (StartDistrictId, EndDistrictId, Distance) " +
                           "VALUES ((SELECT DistrictId FROM Districts WHERE DistrictName = @StartDistrict), " +
                                   "(SELECT DistrictId FROM Districts WHERE DistrictName = @EndDistrict), @Distance)"; // SQL-запрос для добавления связи между районами

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection); // Создание команды для выполнения SQL-запроса
                command.Parameters.AddWithValue("@StartDistrict", startDistrict); // Добавление параметра с названием начального района
                command.Parameters.AddWithValue("@EndDistrict", endDistrict); // Добавление параметра с названием конечного района
                command.Parameters.AddWithValue("@Distance", distance); // Добавление параметра с расстоянием между районами

                try
                {
                    connection.Open(); // Открытие соединения с базой данных
                    int rowsAffected = command.ExecuteNonQuery(); // Выполнение запроса и получение числа затронутых строк

                    // Проверка успешности выполнения запроса и вывод соответствующего сообщения
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Связь между районами успешно добавлена.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении связи между районами.");
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
