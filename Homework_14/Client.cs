using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Homework_14
{
    /// <summary>
    /// Класс клиента банка
    /// </summary>
    public class Client
    {
        #region Поля класса

        Deposit deposit;        //Поле депозитного счета

        Nondeposit nondeposit;  //Поле недепозитного счета

        string name;            //Поле имени

        string surname;         //Поле фамилии

        int age;                //Поле возраста
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="Age">Возраст</param>
        public Client(string Name, string Surname, int Age)
        {
            deposit = new Deposit();
            nondeposit = new Nondeposit();
            name = Name;
            surname = Surname;
            age = Age;
        }

        #region Свойства класса

        public string Name{ get { return name; } set { name = value; } }        //Свойство имени

        public string Surname {get {return surname; } set {surname = value; } } //СВойство Фамилии

        public int Age { get { return age; } set { age = value; } }             //СВойство возраста

        public Deposit Deposit {get {return deposit; }set {deposit = value; } } //СВойство депозитного счета

        public Nondeposit Nondeposit {get{return nondeposit;}set{nondeposit = value;} }//Свойство недепозитного счета
        #endregion
    }
}
