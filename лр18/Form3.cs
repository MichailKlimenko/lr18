using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient; // Импорт пространства имен для работы с SQL Server
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лр18
{
    public partial class Form3 : Form
    {
        // Строка подключения к базе данных
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WolfGGO\Documents\pizza.mdf;Integrated Security=True;Connect Timeout=30";

        public Form3()
        {
            InitializeComponent(); // Инициализация компонентов формы
        }

        // Метод для заполнения комбобокса данными о районах из базы данных
        private void FillDistrictsComboBox()
        {
            string query = "SELECT DistrictName FROM Districts"; // SQL-запрос для получения названий районов из таблицы Districts

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection); // Создание команды для выполнения SQL-запроса

                try
                {
                    connection.Open(); // Открытие соединения с базой данных
                    SqlDataReader reader = command.ExecuteReader(); // Выполнение запроса и получение данных

                    // Чтение данных и заполнение комбобокса
                    while (reader.Read())
                    {
                        string districtName = reader["DistrictName"].ToString();
                        comboBox3.Items.Add(districtName);
                    }

                    reader.Close(); // Закрытие ридера
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Вывод сообщения об ошибке, если произошла ошибка при выполнении запроса
                }
            }
        }

        // Обработчик нажатия кнопки меню "картаToolStripMenuItem"
        private void картаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(); // Создание экземпляра Form4
            form4.ShowDialog(); // Отображение Form4 как диалогового окна
        }

        // Обработчик нажатия кнопки "button1"
        private void button1_Click(object sender, EventArgs e)
        {
            string endDistrict = comboBox3.Text; // Получение названия конечного района из комбобокса

            if (!string.IsNullOrEmpty(endDistrict))
            {
                FindShortestPath(endDistrict); // Вызов метода для поиска кратчайшего пути до указанного района
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите конечный район."); // Вывод сообщения о необходимости выбора конечного района
            }
        }

        // Метод для поиска кратчайшего пути до указанного района
        private void FindShortestPath(string endDistrict)
        {
            string query = "SELECT Distance FROM CityMap " +
                           "WHERE StartDistrictId = 3 " + // Предполагается, что id начального района равен 3
                           "AND EndDistrictId = (SELECT DistrictId FROM Districts WHERE DistrictName = @EndDistrict)"; // SQL-запрос для поиска расстояния до указанного района

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection); // Создание команды для выполнения SQL-запроса
                command.Parameters.AddWithValue("@EndDistrict", endDistrict); // Добавление параметра с названием конечного района

                try
                {
                    connection.Open(); // Открытие соединения с базой данных
                    object result = command.ExecuteScalar(); // Выполнение запроса и получение результата

                    // Обработка результата запроса и вывод соответствующего сообщения
                    if (result != null)
                    {
                        int shortestDistance = (int)result;
                        MessageBox.Show("Кратчайший путь до " + endDistrict + ": " + shortestDistance + " км");
                    }
                    else
                    {
                        MessageBox.Show("Расстояние не найдено.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Вывод сообщения об ошибке, если произошла ошибка при выполнении запроса
                }
            }
        }

        // Обработчик загрузки формы
        private void Form3_Load(object sender, EventArgs e)
        {
            FillDistrictsComboBox(); // Заполнение комбобокса данными о районах при загрузке формы
        }
    }
}
