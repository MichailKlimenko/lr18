using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лр18
{
    public partial class Form5 : Form
    {
        User user;
        Pizza pizza;

        // Конструктор формы
        public Form5()
        {
            InitializeComponent();
            user = new User(); // Создание экземпляра класса User
            pizza = new Pizza(); // Создание экземпляра класса Pizza
        }

        // Класс для работы с отражением типов
        public class Reflector
        {
            // Метод для вывода всего содержимого класса в текстовый файл
            public static void OutputClassContentToFile(string className)
            {
                Type type = Type.GetType(className); // Получение типа класса по его имени
                if (type == null)
                {
                    MessageBox.Show("Класс с именем " + className + " не найден."); // Вывод сообщения об ошибке, если класс не найден
                    return;
                }

                StringBuilder content = new StringBuilder();
                content.AppendLine($"Содержимое класса {className}:"); // Добавление заголовка в содержимое
                content.AppendLine("-----------------------------------------------------");
                foreach (MemberInfo member in type.GetMembers())
                {
                    content.AppendLine(member.ToString()); // Добавление информации о каждом члене класса
                }

                File.WriteAllText($"{className}_content.txt", content.ToString()); // Запись содержимого в файл
                MessageBox.Show($"Содержимое класса {className} успешно выведено в файл {className}_content.txt"); // Вывод сообщения об успешном выполнении
            }

            // Метод для извлечения всех общедоступных публичных методов класса
            public static void ExtractPublicMethods(string className)
            {
                Type type = Type.GetType(className); // Получение типа класса по его имени
                if (type == null)
                {
                    MessageBox.Show("Класс с именем " + className + " не найден."); // Вывод сообщения об ошибке, если класс не найден
                    return;
                }

                MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance); // Получение всех общедоступных публичных методов класса
                if (methods.Length == 0)
                {
                    MessageBox.Show($"В классе {className} нет общедоступных публичных методов."); // Вывод сообщения, если методов нет
                    return;
                }

                StringBuilder methodList = new StringBuilder();
                methodList.AppendLine($"Общедоступные публичные методы класса {className}:"); // Добавление заголовка в список методов
                methodList.AppendLine("-----------------------------------------------------");
                foreach (MethodInfo method in methods)
                {
                    methodList.AppendLine(method.ToString()); // Добавление информации о каждом методе
                }

                MessageBox.Show(methodList.ToString()); // Вывод списка методов
            }

            // Метод для получения информации о полях и свойствах класса
            public static void GetFieldsAndProperties(string className)
            {
                Type type = Type.GetType(className); // Получение типа класса по его имени
                if (type == null)
                {
                    MessageBox.Show("Класс с именем " + className + " не найден."); // Вывод сообщения об ошибке, если класс не найден
                    return;
                }

                StringBuilder fieldsAndPropertiesInfo = new StringBuilder();
                fieldsAndPropertiesInfo.AppendLine($"Информация о полях и свойствах класса {className}:"); // Добавление заголовка
                fieldsAndPropertiesInfo.AppendLine("-----------------------------------------------------");

                FieldInfo[] fields = type.GetFields(); // Получение всех полей класса
                if (fields.Length > 0)
                {
                    fieldsAndPropertiesInfo.AppendLine("Поля:"); // Добавление заголовка для полей
                    foreach (FieldInfo field in fields)
                    {
                        fieldsAndPropertiesInfo.AppendLine(field.ToString()); // Добавление информации о каждом поле
                    }
                }
                else
                {
                    fieldsAndPropertiesInfo.AppendLine("Поля отсутствуют."); // Вывод сообщения, если полей нет
                }

                PropertyInfo[] properties = type.GetProperties(); // Получение всех свойств класса
                if (properties.Length > 0)
                {
                    fieldsAndPropertiesInfo.AppendLine("Свойства:"); // Добавление заголовка для свойств
                    foreach (PropertyInfo property in properties)
                    {
                        fieldsAndPropertiesInfo.AppendLine(property.ToString()); // Добавление информации о каждом свойстве
                    }
                }
                else
                {
                    fieldsAndPropertiesInfo.AppendLine("Свойства отсутствуют."); // Вывод сообщения, если свойств нет
                }

                MessageBox.Show(fieldsAndPropertiesInfo.ToString()); // Вывод информации о полях и свойствах
            }

            // Метод для получения всех реализованных классом интерфейсов
            public static void GetImplementedInterfaces(string className)
            {
                Type type = Type.GetType(className); // Получение типа класса по его имени
                if (type == null)
                {
                    MessageBox.Show("Класс с именем " + className + " не найден."); // Вывод сообщения об ошибке, если класс не найден
                    return;
                }

                Type[] interfaces = type.GetInterfaces(); // Получение всех реализованных интерфейсов класса
                if (interfaces.Length == 0)
                {
                    MessageBox.Show($"Класс {className} не реализует ни один интерфейс."); // Вывод сообщения, если интерфейсов нет
                    return;
                }

                StringBuilder implementedInterfaces = new StringBuilder();
                implementedInterfaces.AppendLine($"Класс {className} реализует следующие интерфейсы:"); // Добавление заголовка
                implementedInterfaces.AppendLine("-----------------------------------------------------");
                foreach (Type iface in interfaces)
                {
                    implementedInterfaces.AppendLine(iface.ToString()); // Добавление информации о каждом интерфейсе
                }

                MessageBox.Show(implementedInterfaces.ToString()); // Вывод списка интерфейсов
            }
        }

        // Обработчик нажатия кнопки для вывода содержимого класса в файл
        private void button1_Click(object sender, EventArgs e)
        {
            string className = comboBox1.Text;
            Reflector.OutputClassContentToFile(className); // Вызов метода для вывода содержимого класса в файл
        }

        // Обработчик нажатия кнопки для извлечения общедоступных методов класса
        private void button2_Click(object sender, EventArgs e)
        {
            string className = comboBox1.Text;
            Reflector.ExtractPublicMethods(className); // Вызов метода для извлечения общедоступных методов класса
        }

        // Обработчик нажатия кнопки для получения информации о полях и свойствах класса
        private void button3_Click(object sender, EventArgs e)
        {
            string className = comboBox1.Text;
            Reflector.GetFieldsAndProperties(className); // Вызов метода для получения информации о полях и свойствах класса
        }

        // Обработчик нажатия кнопки для получения всех реализованных интерфейсов класса
        private void button4_Click(object sender, EventArgs e)
        {
            string className = comboBox1.Text;
            Reflector.GetImplementedInterfaces(className); // Вызов метода для получения всех реализованных интерфейсов класса
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Не реализовано");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Не реализовано");
        }
    }
}
