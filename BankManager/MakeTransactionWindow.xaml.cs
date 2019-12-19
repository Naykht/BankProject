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
        ITransaction tr = Factory.Instance.GTransaction();
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
                senderAcc.Text = $"Account ID(sender): {senderAccSelected.AccId.ToString()}";
                money =
                
            }

        }

        private void selectRecButton_Click(object sender, RoutedEventArgs e)
        {
            var senderAccSelected1 = accSenderCombo.SelectedItem as Account;
            var recAccSelected = accRecCombo.SelectedItem as Account;
            if (recAccSelected != null && recAccSelected != senderAccSelected1)
                recieverAcc.Text = $"Account ID(reciever): {recAccSelected.AccId.ToString()}";
            else if (recAccSelected == senderAccSelected1)
                MessageBox.Show("You can't send money to the same account");
            else
                MessageBox.Show("Please choose an account for the reciever");
        }

        private void ConfrimTranButton_Click(object sender, RoutedEventArgs e)
        {
            decimal am;
            if (!decimal.TryParse(amountBox.Text, out am))
                MessageBox.Show("Invalid input data");
            else if (decimal.Parse(money.Text) < am)
                MessageBox.Show("Account has insufficient funds");
            else
            {
                var senderAccSelected1 = accSenderCombo.SelectedItem as Account;
                int idSen = senderAccSelected1.AccId;
                var recAccSelected = accRecCombo.SelectedItem as Account;
                int idRec = recAccSelected.AccId;
                var date = DateTime.Now;
                var amount = decimal.Parse(money.Text);
                tr.AddTran(idSen, idRec, amount, date);
                UpdateTran?.Invoke();
                Close();
            }
        }
    }
}
