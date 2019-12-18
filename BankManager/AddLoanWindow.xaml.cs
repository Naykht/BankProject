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
        ILoan lo = Factory.Instance.GLoan();
        public event Action<List<Loan>> UpdateLoan; 
        public AddLoanWindow()
        {
            InitializeComponent();
            clientCombo.ItemsSource = acc.Accounts;
        }

        private void MakeLoan_Click(object sender, RoutedEventArgs e)
        {
            var accId = clientCombo.SelectedItem as Account;
            var en = EndBox.SelectedDate;
            decimal am;
            decimal p;
            if ( en != null && Decimal.TryParse(amountBox.Text, out am) && decimal.TryParse(percentBox.Text, out p) && accId != null)
            {
                var now = DateTime.Now;
                lo.AddLoan(accId.AccId, am, en ?? now, p);
                UpdateLoan?.Invoke(lo.Loans);
                MessageBox.Show("You've successfully added a new loan");
                Close();
               
            }
            else
                MessageBox.Show("Invalid input data");
        }
    }
}
