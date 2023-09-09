using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_14
{
    /// <summary>
    /// Контрвариатный интерфейс перевода денег между счетами
    /// </summary>
    /// <typeparam name="T">Счет</typeparam>
    interface ITransfer<in T>
    {
        void Transfer(int money, T account);
    }
}
