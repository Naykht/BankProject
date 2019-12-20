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
        private void MakeDeposit_Click(object sender, RoutedEventArgs e)//kirill
        {
            var now = DateTime.Now;
            var en = EndBox.SelectedDate;
            decimal am;
            decimal p;
            if (curAcc.Text == "-")
                MessageBox.Show("Please choose an account");
            else if (en == null || !decimal.TryParse(amountBox.Text, out am) || !decimal.TryParse(percentBox.Text, out p))
                MessageBox.Show("Invalid input data");
            else if (decimal.Parse(money.Text) < am)
                MessageBox.Show("Account has insufficient funds");
            else if (en < now.AddYears(1))
                MessageBox.Show("Minimum holding period of deposit is one year!");
            else
            {
                IDeposit dep = Factory.Instance.GDeposit();
                int accId = int.Parse(curAcc.Text);
                dep.AddDeposit(accId, am, en ?? now, p);
                UpdateDeposit?.Invoke();
                curAcc.Text = null;
                money.Text = null;
                amountBox.Text = null;
                percentBox.Text = null;
                EndBox.SelectedDate = null;
                clientCombo.SelectedItem = null;
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
