using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_14
{
    /// <summary>
    /// Параметризированный класс, который принимает в качестве входящего параметра любой счет и задает модель работы с ним.
    /// </summary>
    /// <typeparam name="T">Счет</typeparam>
    class Session<T> : IRefill<T>, ITransfer<T>, IType<T>
        where T: Account
    {
        /// <summary>
        /// Поле текущий счет
        /// </summary>
        T sessionAccount;

        /// <summary>
        /// Свойство текущий счет
        /// </summary>
        public T SessionAccount { get { return sessionAccount; } }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Account"></param>
        public Session(T Account)
        {
            sessionAccount = Account;
        }

        #region Методы

        /// <summary>
        /// Метод открытия счета
        /// </summary>
        public void Open()
        {
            sessionAccount.Status = true;
        }

        /// <summary>
        /// Метод закрытия счета
        /// </summary>
        public void Close()
        {
            sessionAccount.Status = false;
        }

        /// <summary>
        /// Метод пополнения счета. Реализует ковариантный интерфейс.
        /// </summary>
        /// <param name="Amount">Сумма</param>
        /// <returns></returns>
        public T Refill(int money)
        {
            sessionAccount.Balance += money;
            return sessionAccount;
        }

        /// <summary>
        /// Метод перевода денег между счетами. Реализует контрвариантный интерфейс.
        /// </summary>
        /// <param name="Amount"></param>
        /// <param name="SendersAccount"></param>
        public void Transfer(int money, T senderAccount)
        {
            senderAccount.Balance -= money;
            sessionAccount.Balance += money;
        }
        #endregion
    }
}
