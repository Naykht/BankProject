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
    /// Логика взаимодействия для AddDeposit.xaml
    /// </summary>
    public partial class AddDeposit : Window
    {
        IAccount acc = Factory.Instance.GAccount();
        IDeposit dep = Factory.Instance.GDeposit();
        public event Action UpdateDeposit;
        public event Action UpdateAccount;
        Account choCl;
        public AddDeposit()
        {
            InitializeComponent();
            clientCombo.ItemsSource = acc.Accounts;
        }
        private void SelectClient_Click(object sender, RoutedEventArgs e)
        {
            choCl = clientCombo.SelectedItem as Account;
            curAcc.Text = choCl.AccId.ToString();
            money.Text = choCl.Balance.ToString();
        }
        private void MakeDeposit_Click(object sender, RoutedEventArgs e)
        {
            var en = EndBox.SelectedDate;
            decimal am;
            decimal p;
            if (curAcc.Text == "-")
                MessageBox.Show("Please choose an account");
            else if (en == null || !decimal.TryParse(amountBox.Text, out am) || !decimal.TryParse(percentBox.Text, out p))
                MessageBox.Show("Invalid input data");
            else if (decimal.Parse(money.Text) < am)
                MessageBox.Show("Account has insufficient funds");
            else
            {
                var now = DateTime.Now;
                int accId = int.Parse(curAcc.Text);
                dep.AddDeposit(accId, am, en ?? now, p);
                UpdateDeposit?.Invoke();
                acc.Money(accId, am, false);
                UpdateAccount?.Invoke();
                Close();
                MessageBox.Show("You've successfully added a new deposit");
            }
        }
        private void ClientSelect_Click(object sender, RoutedEventArgs e)
        {
            choCl = clientCombo.SelectedItem as Account;
            curAcc.Text = choCl.AccId.ToString();
            money.Text = choCl.Balance.ToString();
        }
    }
}
