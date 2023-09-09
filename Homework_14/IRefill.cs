using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_14
{
    /// <summary>
    /// Ковариантный интерфейс пополнения счета
    /// </summary>
    /// <typeparam name="T">Счет</typeparam>
    interface IRefill<out T>
    {
        T Refill(int money);
    }
}
