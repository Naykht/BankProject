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
        public MakeTransactionWindow()
        {
            InitializeComponent();
            accSenderCombo.ItemsSource = acc.Accounts;
            accRecCombo.ItemsSource = acc.Accounts;
        }


        private void selectSenButton_Click(object sender, RoutedEventArgs e)
        {
            
            var senderAccSelected = accSenderCombo.SelectedItem as Account;
            if (senderAccSelected == null)
                MessageBox.Show("Please choose an account for the sender");
            else
            {
                senderAcc.Text = senderAccSelected.AccId.ToString();
                money.Text = senderAccSelected.Balance.ToString();
            }
        }

        private void selectRecButton_Click(object sender, RoutedEventArgs e)
        {
            var recAccSelected = accRecCombo.SelectedItem as Account;
            if (recAccSelected == null)
                MessageBox.Show("Please choose an account for the receiver");
            else
                recieverAcc.Text = recAccSelected.AccId.ToString();
            
        }

        private void ConfrimTranButton_Click(object sender, RoutedEventArgs e)
        {
            decimal am;
            if (!decimal.TryParse(amountBox.Text, out am))
                MessageBox.Show("Invalid input data");
            else if (decimal.Parse(money.Text) < am)
                MessageBox.Show("Account has insufficient funds");
            else if (senderAcc.Text == recieverAcc.Text)
                MessageBox.Show("You cannot idk");
            else
            {
                ITransaction tr = Factory.Instance.GTransaction();
                tr.AddTran(int.Parse(senderAcc.Text), int.Parse(recieverAcc.Text), am);
                UpdateTran?.Invoke();
                MessageBox.Show("Transaction has been successfully made");
                amountBox.Text = null;
                accRecCombo.SelectedItem = null;
                accSenderCombo.SelectedItem = null;
                money.Text = null;
                senderAcc.Text = null;
                recieverAcc.Text = null;
            }
        }
    }
}
