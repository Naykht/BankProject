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
    /// Логика взаимодействия для AddLoanWindow.xaml
    /// </summary>
    public partial class AddLoanWindow : Window
    {
        IAccount acc = Factory.Instance.GAccount();
        public event Action UpdateLoan;
        Account choCl;
        public AddLoanWindow()
        {
            InitializeComponent();
            clientCombo.ItemsSource = acc.Accounts;
        }

        private void MakeLoan_Click(object sender, RoutedEventArgs e)
        {
            var en = EndBox.SelectedDate;
            decimal am;
            decimal p;
            if (choCl == null)
                MessageBox.Show("Please choose an account");
            else if (en == null || !decimal.TryParse(amountBox.Text, out am) || !decimal.TryParse(percentBox.Text.Replace(".",","), out p))
                MessageBox.Show("Invalid input data");
            else
            {
                ILoan lo = Factory.Instance.GLoan();
                var now = DateTime.Now;
                int accId = choCl.AccId;
                lo.AddLoan(accId, am, en ?? now, p);
                UpdateLoan?.Invoke();
                amountBox.Text = "30000";
                percentBox.Text = "8";
                EndBox.SelectedDate = null;
                clientCombo.SelectedItem = null;
                MessageBox.Show("You've successfully added a new loan");
            }
        }
        private void clientCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            choCl = clientCombo.SelectedItem as Account;
        }
    }
}
