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
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }
        public ClientWindow(Client client)
        {
            InitializeComponent();
            NameClient.Text = client.Name;
            SurnameClient.Text = client.Surname;
            AgeClient.Text = client.Age.ToString();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
