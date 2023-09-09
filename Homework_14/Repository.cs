using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_14
{
    /// <summary>
    /// Статический класс, хранящий базу клиентов банка и определяющий методы работы с ней
    /// </summary>
    public static class Repository
    {
        /// <summary>
        /// Статическое поле базы клиентов
        /// </summary>
        static List<Client> clients;

        /// <summary>
        /// Конструктор
        /// </summary>
        static Repository()
        {
            clients = new List<Client>();
        }

        /// <summary>
        /// Статическое свойство базы клиентов
        /// </summary>
        public static List<Client> Clients { get { return clients;} set {clients = value;} }

        #region Методы

        /// <summary>
        /// Метод, открывающий базу клиентов
        /// </summary>
        public static void OpenRepository()
        {
            try { clients = JsonConvert.DeserializeObject<List<Client>>(File.ReadAllText("Repository.json")); }
            catch { }
        }

        /// <summary>
        /// Метод, сохраняющий базу клиентов
        /// </summary>
        public static void SaveRepository()
        {
            File.WriteAllText("Repository.json", JsonConvert.SerializeObject(Clients));
        }

        /// <summary>
        /// Метод, добавляющий клиента в базу
        /// </summary>
        /// <param name="client"></param>
        public static void AddClient(Client client)
        {
            clients.Add(client);
        }

        /// <summary>
        /// Метод, удаляющий клиента из базы
        /// </summary>
        /// <param name="client"></param>
        public static void DeleteClient(Client client)
        {
            clients.Remove(client);
        }

        /// <summary>
        /// Метод, редактирующий клиента в базе
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="number">Номер клиента</param>
        public static void EditClient(Client client, int number)
        {
            clients.RemoveAt(number);
            clients.Insert(number, client);
        }
        #endregion
    }
}
