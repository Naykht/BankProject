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
        public event Action UpdateDeposit;
        Account choCl;
        public AddDeposit()
        {
            InitializeComponent();
            clientCombo.ItemsSource = acc.Accounts;
        }
        private void MakeDeposit_Click(object sender, RoutedEventArgs e)
        {
            var en = EndBox.SelectedDate;
            decimal am;
            decimal p;
            if (choCl == null)
                MessageBox.Show("Please choose an account");
            else if (en == null || !decimal.TryParse(amountBox.Text, out am) || !decimal.TryParse(percentBox.Text.Replace(".", ","), out p))
                MessageBox.Show("Invalid input data");
            else if (choCl.Balance < am)
                MessageBox.Show("Account has insufficient funds");
            else
            {
                IDeposit dep = Factory.Instance.GDeposit();
                var now = DateTime.Now;
                int accId = choCl.AccId;
                dep.AddDeposit(accId, am, en ?? now, p);
                UpdateDeposit?.Invoke();
                amountBox.Text = "30000";
                percentBox.Text = "3";
                EndBox.SelectedDate = null;
                clientCombo.SelectedItem = null;
                MessageBox.Show("You've successfully added a new deposit");
            }
        }
        private void clientCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            choCl = clientCombo.SelectedItem as Account;
            if (choCl == null)
                money.Text = "0";
            else
                money.Text = choCl.Balance.ToString();
        }
    }
}
