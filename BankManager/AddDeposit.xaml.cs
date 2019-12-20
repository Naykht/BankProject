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
            Update();
        }
        private void MakeDeposit_Click(object sender, RoutedEventArgs e)
        {
            var en = EndBox.SelectedDate;
            decimal am;
            decimal p;
            var now = DateTime.Now;
            if (choCl == null)
                MessageBox.Show("Please choose an account");
            else if (en == null || !decimal.TryParse(amountBox.Text, out am) || !decimal.TryParse(percentBox.Text.Replace(".", ","), out p))
                MessageBox.Show("Invalid input data");
            else if (choCl.Balance < am)
                MessageBox.Show("Account has insufficient funds");
            else if ((en ?? now) < now.AddDays(180))
                MessageBox.Show("Minimum deposit period is at least 180 days");
            else
            {
                IDeposit dep = Factory.Instance.GDeposit();
                int accId = choCl.AccId;
                dep.AddDeposit(accId, am, en ?? now, p);
                UpdateDeposit?.Invoke();
                amountBox.Text = "30000";
                percentBox.Text = "3";
                EndBox.SelectedDate = null;
                clientCombo.SelectedItem = null;
                MessageBox.Show("You've successfully added a new deposit");
                Update();
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

        public void Update()
        {
            acc = Factory.Instance.GAccount();
            clientCombo.ItemsSource = acc.Accounts;
        }
    }
}
