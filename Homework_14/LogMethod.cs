using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Homework_14
{
    /// <summary>
    /// Класс записи лога событий
    /// </summary>
    static class LogMethod
    {
        /// <summary>
        /// Метод записи события с параметрами
        /// </summary>
        /// <param name="procedure">Действие</param>
        /// <param name="dateTime">Время</param>
        /// <param name="employee">Сотрудник</param>
        /// <param name="client">Клиент</param>
        public static void PrintEventToLog(string procedure, DateTime dateTime, string employee, Client client)
        {
            File.AppendAllText("Log.txt", $"{employee} совершил действие {procedure} по имени {client.Name} {client.Surname} в {dateTime}\n");
            MessageBox.Show($"{employee} совершил действие {procedure} по имени {client.Name} {client.Surname} в {dateTime}");
        }

        /// <summary>
        /// Перегрузка метод записи события с другими параметрами
        /// </summary>
        /// <param name="procedure">Дествие</param>
        /// <param name="dateTime">Время</param>
        /// <param name="employee">Сотрудник</param>
        public static void PrintEventToLog(string procedure, DateTime dateTime, string employee)
        {
            File.AppendAllText("Log.txt", $"{employee} совершил действие {procedure} в {dateTime}\n");
            MessageBox.Show($"{employee} совершил действие {procedure} в {dateTime}");
        }

        /// <summary>
        /// Перегрузка метода записи события с другими параметрами
        /// </summary>
        /// <param name="procedure">Действие</param>
        /// <param name="dateTime">Время</param>
        /// <param name="employee">Сотрудник</param>
        /// <param name="client">Клиент</param>
        /// <param name="type">Тип счета</param>
        public static void PrintEventToLog(string procedure, DateTime dateTime, string employee, Client client, IType<Account> type)
        {
            File.AppendAllText("Log.txt", $"{employee} совершил действие {procedure} cо счетом {type.SessionAccount} клиента {client.Name} {client.Surname} в {dateTime}");
            MessageBox.Show($"{employee} совершил действие {procedure} cо счетом {type.SessionAccount} клиента {client.Name} {client.Surname} в {dateTime}");
        }
    }
}
