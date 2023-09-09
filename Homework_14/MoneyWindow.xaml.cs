using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Homework_14
{
    /// <summary>
    /// Логика взаимодействия для MoneyWindow.xaml
    /// </summary>
    public partial class MoneyWindow : Window
    {
        public MoneyWindow()
        {
            InitializeComponent();
        }

        private void OKMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
