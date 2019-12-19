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
using banks;
using banks.Model;

namespace BankManager
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        IClient cl = Factory.Instance.GClient();
        IAccount acc = Factory.Instance.GAccount();
        ILoan lo = Factory.Instance.GLoan();
        IDeposit dep = Factory.Instance.GDeposit();
        ITransaction tr = Factory.Instance.GTransaction();
        public MainMenu()
        {
            InitializeComponent();
            UpdateList();
            UpdateCombo();
        }
        public void UpdateList()
        {
   
            clientList.ItemsSource = null;
            clientList.ItemsSource = cl.Clients;
            accountList.ItemsSource = null;
            accountList.ItemsSource = acc.Accounts;
            loanList.ItemsSource = null;
            lo.StatusLoan();
            loanList.ItemsSource = lo.Loans;
            depList.ItemsSource = null;
            depList.ItemsSource = dep.Deposits;
            tranList.ItemsSource = null;
            tranList.ItemsSource = tr.Transactions;
       
        }
        public void UpdateCombo()
        {
            List<string> sample = new List<string>()
            {
                "All",
                "Active",
                "Closed",
                "Expired"
            };
            choiceLoan.ItemsSource = sample;
            
            choiceLoan.SelectedItem = "All";
        }
        private void DateLoan_Click(object sender, RoutedEventArgs e)
        {
            var now = DateTime.Now;
            if (lStartBox.SelectedDate != null && lEndBox.SelectedDate != null && lStartBox.SelectedDate < lEndBox.SelectedDate)
            {
                loanList.ItemsSource = null;
                loanList.ItemsSource = lo.DateLoan(lStartBox.SelectedDate ?? now, lEndBox.SelectedDate ?? now, choiceLoan.SelectedItem as string);
            }
            else
                MessageBox.Show("Please select an arbitrary period");
        }
        private void ResetLoan_Click(object sender, RoutedEventArgs e)
        {
            lStartBox.SelectedDate = null;
            lEndBox.SelectedDate = null;
            loanList.ItemsSource = lo.Loans;
            UpdateList();
        }
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var winClAdd = new AddClientWindow();
            winClAdd.UpdateClient += UpdateClient; 
            winClAdd.Show();
        }
        public void UpdateClient()
        {
            clientList.ItemsSource = null;
            cl = Factory.Instance.GClient();
            clientList.ItemsSource = cl.Clients;
        }
        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            
            Client cl = clientList.SelectedItem as Client;
            if (cl != null)
            {
                var winClEd = new EditClientWindow(cl);
                winClEd.UpdateClient += UpdateClient;
                winClEd.Show();
            }
            else
                MessageBox.Show("Please, choose a client");
            
        }
        private void MoreClient_Click(object sender, RoutedEventArgs e)
        {
            Client cl = clientList.SelectedItem as Client;
            if (cl != null)
            {
                var winClMo = new MoreClientWindow(cl);
                winClMo.Show();
            }
        }
        private void ExecuteDep_Click(object sender, RoutedEventArgs e)
        { 
            if (dStartBox.SelectedDate != null && dEndBox.SelectedDate != null && dStartBox.SelectedDate <= dEndBox.SelectedDate)
            {
                var now = DateTime.Now;
                depList.ItemsSource = null;
                depList.ItemsSource = dep.DateDep(dStartBox.SelectedDate ?? now, dEndBox.SelectedDate ?? now);
            }
             else   
                MessageBox.Show("Please select an arbitrary period");
        }
        private void ResetDep_Click(object sender, RoutedEventArgs e)
        {
            depList.ItemsSource = null;
        }

        private void MakeDep_Click(object sender, RoutedEventArgs e)
        {
            var winAddDep = new AddDeposit();
            winAddDep.Show();
        }

        private void MakeLoan_Click(object sender, RoutedEventArgs e)
        {
            var winAddLoan = new AddLoanWindow();
            winAddLoan.UpdateLoan += UpdateLoan;
            winAddLoan.Show();
        }
        public void UpdateLoan()
        {
            loanList.ItemsSource = null;
            lo = Factory.Instance.GLoan();
            loanList.ItemsSource = lo.Loans;
        }

        private void CloseLoan_Click(object sender, RoutedEventArgs e)
        {
            var closeLo = loanList.SelectedItem as Loan;
            if (closeLo != null)
            {
                lo.CloseLoan(closeLo.LoanId);
                UpdateLoan();
            }
            else
                MessageBox.Show("Please select a loan");
        }
        public void UpdateAccount()
        {
            accountList.ItemsSource = null;
            acc = Factory.Instance.GAccount();
            accountList.ItemsSource = acc.Accounts;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            IAccount ac = Factory.Instance.GAccount();
            var searchId = SearchAccgBox.Text;
            if (searchId != "")
                accountList.ItemsSource = ac.ClAcc(int.Parse(searchId));
            else
                MessageBox.Show("Please, enter correct ID");
        }

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            var accAddWin = new AddAccountWindow();
            accAddWin.UpdateAccount += UpdateAccount;
            accAddWin.Show();
        }
    }
}
