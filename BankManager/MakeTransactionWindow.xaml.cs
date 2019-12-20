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
    /// Логика взаимодействия для MakeTransactionWindow.xaml
    /// </summary>
    public partial class MakeTransactionWindow : Window
    {
        IAccount acc = Factory.Instance.GAccount();
        public event Action UpdateTran;
        Account recAcc;
        Account senAcc;
        public MakeTransactionWindow()
        {
            InitializeComponent();
            accSenderCombo.ItemsSource = acc.Accounts;
            accRecCombo.ItemsSource = acc.Accounts;
        }
        private void ConfrimTranButton_Click(object sender, RoutedEventArgs e)
        {
            decimal am;
            if (recAcc == null || senAcc == null)
                MessageBox.Show("Please select sender and receiver accounts");
            else if (!decimal.TryParse(amountBox.Text, out am))
                MessageBox.Show("Invalid input data");
            else if (decimal.Parse(money.Text) < am)
                MessageBox.Show("Account has insufficient funds");
            else if (senAcc.AccId == recAcc.AccId)
                MessageBox.Show("You cannot idk");
            else
            {
                ITransaction tr = Factory.Instance.GTransaction();
                tr.AddTran(senAcc.AccId, recAcc.AccId, am);
                UpdateTran?.Invoke();
                MessageBox.Show("Transaction has been successfully made");
                amountBox.Text = "0";
                accRecCombo.SelectedItem = null;
                accSenderCombo.SelectedItem = null;
                
            }
        }
        private void accSenderCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            senAcc = accSenderCombo.SelectedItem as Account;
            if (senAcc == null)
                money.Text = null;
            else
                money.Text = senAcc.Balance.ToString();
        }
        private void accRecCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            recAcc = accRecCombo.SelectedItem as Account;
        }
    }
}
