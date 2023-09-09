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
    /// Логика взаимодействия для BankWindow.xaml
    /// </summary>
    public partial class BankWindow : Window
    {
        #region Поля

        string employee;
        ClientWindow clientWindow;
        MoneyWindow moneyWindow;
        ClientTransferWindow clientTransferWindow;
        IRefill<Account> refill;
        IType<Account> type;
        Session<Account> session;
        event Action<string, DateTime, string> RepositoryEvent;
        event Action<string, DateTime, string, Client> ClientEvent;
        event Action<string, DateTime, string, Client, IType<Account>> AccountEvent;
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public BankWindow(string Employee)
        {
            employee = Employee;
            InitializeComponent();
            ClientsListView.ItemsSource = Repository.Clients;
        }

        #region Обработчики событий и методы

        /// <summary>
        /// Обработчик события нажатия кнопки Открыть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenRepositoryButton_Click(object sender, RoutedEventArgs e)
        {
            Repository.OpenRepository();
            ClientsListView.ItemsSource = Repository.Clients;
            RepositoryEvent += LogMethod.PrintEventToLog;
            RepositoryEvent.Invoke("Open", DateTime.Now, employee);
            RepositoryEvent -= LogMethod.PrintEventToLog;
        }

        /// <summary>
        /// Обработчик события кнопки Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveRepositoryButton_Click(object sender, RoutedEventArgs e)
        {
            Repository.SaveRepository();
            RepositoryEvent += LogMethod.PrintEventToLog;
            RepositoryEvent.Invoke("Save", DateTime.Now, employee);
            RepositoryEvent -= LogMethod.PrintEventToLog;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            new LogWindow("Log.txt").Show();
        }

        /// <summary>
        /// Обработчик события кнопки Пополнения счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefillButton_Click(object sender, RoutedEventArgs e)
        {
            moneyWindow = new MoneyWindow();
            moneyWindow.OKMoneyButton.Click += OKButtonRefillClient;
            if (DepositAccountListView.SelectedItem != null && (DepositAccountListView.SelectedItem as Deposit).Status)
            {
                type = new Session<Deposit>(DepositAccountListView.SelectedItem as Deposit); 
                refill = new Session<Deposit>(DepositAccountListView.SelectedItem as Deposit);
                moneyWindow.Show();
            }
            else if (NonDepositAccountListView.SelectedItem != null && (NonDepositAccountListView.SelectedItem as Nondeposit).Status)
            {
                type = new Session<Nondeposit>(NonDepositAccountListView.SelectedItem as Nondeposit);
                refill = new Session<Nondeposit>(NonDepositAccountListView.SelectedItem as Nondeposit);
                moneyWindow.Show();
            }
        }

        /// <summary>
        /// Метод, подписанный на событие нажатия кнопки ОК в окне ввода суммы денег
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButtonRefillClient(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(moneyWindow.MoneyBox.Text, out int money))
            {
                refill.Refill(money);
                AccountEvent += LogMethod.PrintEventToLog;
                AccountEvent.Invoke("Refill", DateTime.Now, employee, ClientsListView.SelectedItem as Client, type);
                AccountEvent -= LogMethod.PrintEventToLog;
                DepositAccountListView.Items.Refresh();
                NonDepositAccountListView.Items.Refresh();
            }
        }

        /// <summary>
        /// Обработчик события кнопки Добавить клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            clientWindow = new ClientWindow();
            clientWindow.OKButton.Click += AddClient;
            clientWindow.Show();
        }

        /// <summary>
        /// Метод, подписанный на событие нажатия кнопки ОК в окне добавления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClient(object sender, RoutedEventArgs e)
        {
            Client client = new Client(clientWindow.NameClient.Text, clientWindow.SurnameClient.Text, Convert.ToInt32(clientWindow.AgeClient.Text));
            Repository.AddClient(client);
            ClientsListView.Items.Refresh();
            ClientEvent += LogMethod.PrintEventToLog;
            ClientEvent.Invoke("Add Client", DateTime.Now, employee, client);
            ClientEvent -= LogMethod.PrintEventToLog;
        }

        /// <summary>
        /// Обработчик события кнопки Редактировать клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsListView.SelectedItem != null)
            {
                clientWindow = new ClientWindow(ClientsListView.SelectedItem as Client);
                clientWindow.OKButton.Click += EditClient;
                clientWindow.Show();
            }
        }

        /// <summary>
        /// Метод, подписанный на событие нажатия клавиши ОК в окне редактирования клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClient(object sender, RoutedEventArgs e)
        {
            Client client = new Client(clientWindow.NameClient.Text, clientWindow.SurnameClient.Text, Convert.ToInt32(clientWindow.AgeClient.Text));
            Repository.EditClient(client, ClientsListView.SelectedIndex);
            ClientsListView.Items.Refresh();
            ClientEvent += LogMethod.PrintEventToLog;
            ClientEvent.Invoke("Edit Client", DateTime.Now, employee, client);
            ClientEvent -= LogMethod.PrintEventToLog;
        }

        /// <summary>
        /// Обработчик события кнопки Удалить клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsListView.SelectedItem != null)
            {
                ClientEvent += LogMethod.PrintEventToLog;
                ClientEvent.Invoke("Delete Client", DateTime.Now, employee, Repository.Clients[ClientsListView.SelectedIndex]);
                Repository.Clients.RemoveAt(ClientsListView.SelectedIndex);
                ClientsListView.Items.Refresh();
            }
        }

        /// <summary>
        /// Обработчик события выбора элемента в листе Недепозитного счета, со снятием выделения в листе Депозитного счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsListView.SelectedItem != null)
            {
                DepositAccountListView.Items.Clear();
                DepositAccountListView.Items.Add((ClientsListView.SelectedItem as Client).Deposit);
                NonDepositAccountListView.Items.Clear();
                NonDepositAccountListView.Items.Add((ClientsListView.SelectedItem as Client).Nondeposit);
            }
        }

        /// <summary>
        /// Обработчик события кнопки открытия счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepositAccountListView.SelectedItem != null && !(DepositAccountListView.SelectedItem as Account).Status)
            {
                Session<Deposit> session = new Session<Deposit>(DepositAccountListView.SelectedItem as Deposit);
                session.Open();
                DepositAccountListView.Items.Refresh();
                AccountEvent += LogMethod.PrintEventToLog;
                AccountEvent.Invoke("Open", DateTime.Now, employee, ClientsListView.SelectedItem as Client, session);
                AccountEvent -= LogMethod.PrintEventToLog;
            }
            else if (NonDepositAccountListView.SelectedItem != null && !(NonDepositAccountListView.SelectedItem as Account).Status)
            {
                Session<Nondeposit> session = new Session<Nondeposit>(NonDepositAccountListView.SelectedItem as Nondeposit);
                session.Open();
                NonDepositAccountListView.Items.Refresh();
                AccountEvent += LogMethod.PrintEventToLog;
                AccountEvent.Invoke("Open", DateTime.Now, employee, ClientsListView.SelectedItem as Client, session);
                AccountEvent -= LogMethod.PrintEventToLog;
            }
        }

        /// <summary>
        /// Обработчик события кнопки закрытия счета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepositAccountListView.SelectedItem != null && (DepositAccountListView.SelectedItem as Account).Status)
            {
                Session<Deposit> session = new Session<Deposit>(DepositAccountListView.SelectedItem as Deposit);
                session.Close();
                DepositAccountListView.Items.Refresh();
                AccountEvent += LogMethod.PrintEventToLog;
                AccountEvent.Invoke("Close", DateTime.Now, employee, ClientsListView.SelectedItem as Client, session);
                AccountEvent -= LogMethod.PrintEventToLog;
            }
            else if (NonDepositAccountListView.SelectedItem != null && (NonDepositAccountListView.SelectedItem as Account).Status)
            {
                Session<Nondeposit> session = new Session<Nondeposit>(NonDepositAccountListView.SelectedItem as Nondeposit);
                session.Close();
                NonDepositAccountListView.Items.Refresh();
                AccountEvent += LogMethod.PrintEventToLog;
                AccountEvent.Invoke("Close", DateTime.Now, employee, ClientsListView.SelectedItem as Client, session);
                AccountEvent -= LogMethod.PrintEventToLog;
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки Перевода денег со счета на счет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            if(DepositAccountListView.SelectedItem != null && (DepositAccountListView.SelectedItem as Account).Status)
            {
                clientTransferWindow = new ClientTransferWindow();
                clientTransferWindow.Show();
                clientTransferWindow.OKTransferButton.Click += OKClientTransferWindowButton;
            }
            else if (NonDepositAccountListView.SelectedItem != null && (NonDepositAccountListView.SelectedItem as Account).Status)
            {
                clientTransferWindow = new ClientTransferWindow();
                clientTransferWindow.Show();
                clientTransferWindow.OKTransferButton.Click += OKClientTransferWindowButton;
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки ОК в окне выбора клиента для перевода денег
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKClientTransferWindowButton(object sender, RoutedEventArgs e)
        {
            if (clientTransferWindow.ClientsListView.SelectedItem != null)
            {
                moneyWindow = new MoneyWindow();
                moneyWindow.Show();
                moneyWindow.OKMoneyButton.Click += OKMoneyTransferWindowButton;
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки ОК в окне выбора суммы денег для перевода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKMoneyTransferWindowButton(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(moneyWindow.MoneyBox.Text, out int money))
            {
                if (DepositAccountListView.SelectedItem != null)
                {
                    type = new Session<Account>(DepositAccountListView.SelectedItem as Account);
                    ITransfer<Account> transfer = new Session<Account>(DepositAccountListView.SelectedItem as Account);
                    transfer.Transfer(money, (clientTransferWindow.ClientsListView.SelectedItem as Client).Deposit);
                }
                else if(NonDepositAccountListView.SelectedItem != null)
                {
                    type = new Session<Account>(NonDepositAccountListView.SelectedItem as Account);
                    ITransfer<Account> transfer = new Session<Account>(NonDepositAccountListView.SelectedItem as Account);
                    transfer.Transfer(money, (clientTransferWindow.ClientsListView.SelectedItem as Client).Nondeposit);
                }
            }
            AccountEvent += LogMethod.PrintEventToLog;
            AccountEvent.Invoke("Transfer", DateTime.Now, employee, ClientsListView.SelectedItem as Client, type);
            AccountEvent -= LogMethod.PrintEventToLog;
            clientTransferWindow.Close();
            DepositAccountListView.Items.Refresh();
            NonDepositAccountListView.Items.Refresh();
        }
        #endregion
    }
}