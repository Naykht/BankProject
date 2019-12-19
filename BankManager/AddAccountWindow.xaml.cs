using banks;
using banks.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankManager
{
    /// <summary>
    /// Логика взаимодействия для AddAccountWindow.xaml
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        IClient cl = Factory.Instance.GClient();
        public event Action UpdateAccount;
        public AddAccountWindow()
        {
            InitializeComponent();
            clientCombo.ItemsSource = cl.Clients;   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal startBalance;
            var chosenClient = clientCombo.SelectedItem as Client;
            if (decimal.TryParse(balanceBox.Text, out startBalance) && chosenClient != null)
            {
                IAccount acc = Factory.Instance.GAccount();
                acc.AddAccount(chosenClient.Id, startBalance);
                UpdateAccount?.Invoke();
                balanceBox.Text = null;
                clientCombo.SelectedItem = null;
                MessageBox.Show("You've successfully added a new account");
            }
            else
                MessageBox.Show("Invalid input data");
        }
    }
}
