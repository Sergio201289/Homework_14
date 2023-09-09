using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_14
{
    /// <summary>
    /// Контрвариантный интерфейс возвращающий счет клиента
    /// </summary>
    /// <typeparam name="T">Счет</typeparam>
    interface IType<out T>
    {
        T SessionAccount { get; }
    }
}
