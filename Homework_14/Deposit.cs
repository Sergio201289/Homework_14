using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_14
{
    /// <summary>
    /// Класс, наследующий от абстрактного класса счета
    /// </summary>
    public class Deposit : Account
    {
        public Deposit()
        {
            Type = "Deposit";
        }
    }
}
