using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_14
{
    /// <summary>
    /// Класс, наследующий абстрактный класс счета
    /// </summary>
    public class Nondeposit : Account
    {
        public Nondeposit()
        {
            Type = "Nondeposit";
        }
    }
}
