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
            lo.StatusLoan();
            dep.StatusDeposit();
            clientList.ItemsSource = null;
            clientList.ItemsSource = cl.Clients;
            accountList.ItemsSource = null;
            accountList.ItemsSource = acc.Accounts;
            loanList.ItemsSource = null;
            loanList.ItemsSource = lo.Loans;
            depList.ItemsSource = null;
            depList.ItemsSource = dep.Deposits;
            tranList.ItemsSource = null;
            tranList.ItemsSource = tr.Transactions;
        }
        public void UpdateCombo()//
        {
            choiceLoan.ItemsSource = new List<string>()
            {
                "All",
                "Active",
                "Repaid",
                "Expired"
            };
            choiceLoan.SelectedItem = "All";
            choiceDepo.ItemsSource = new List<string>()
            {
                "All",
                "Active",
                "Closed"
            };
            choiceDepo.SelectedItem = "All";
        }
        private void DateLoan_Click(object sender, RoutedEventArgs e)//
        {
            if (lStartBox.SelectedDate != null && lEndBox.SelectedDate != null && lStartBox.SelectedDate < lEndBox.SelectedDate)
            {
                var now = DateTime.Now;
                loanList.ItemsSource = null;
                loanList.ItemsSource = lo.DateLoan(lStartBox.SelectedDate ?? now, lEndBox.SelectedDate ?? now, choiceLoan.SelectedItem as string);
            }
            else
                MessageBox.Show("Please select a correct arbitrary period");
        }
        private void ResetLoan_Click(object sender, RoutedEventArgs e)//
        {
            lStartBox.SelectedDate = null;
            lEndBox.SelectedDate = null;
            UpdateLoan();
        }
        private void AddClient_Click(object sender, RoutedEventArgs e)//
        {
            var winClAdd = new AddClientWindow();
            winClAdd.UpdateClient += UpdateClient; 
            winClAdd.Show();
        }
        public void UpdateClient()//
        {
            clientList.ItemsSource = null;
            cl = Factory.Instance.GClient();
            clientList.ItemsSource = cl.Clients;
        }
        private void EditClient_Click(object sender, RoutedEventArgs e)//
        {
            Client cl = clientList.SelectedItem as Client;
            if (cl != null)
            {
                var winClEd = new EditClientWindow(cl);
                winClEd.UpdateClient += UpdateClient;
                winClEd.Show();
            }
            else
                MessageBox.Show("Please, select a client");
        }
        private void MoreClient_Click(object sender, RoutedEventArgs e)//
        {
            Client cl = clientList.SelectedItem as Client;
            if (cl != null)
            {
                var winClMo = new MoreClientWindow(cl);
                winClMo.Show();
            }
            else
                MessageBox.Show("Please, select a client");
        }
        private void ExecuteDep_Click(object sender, RoutedEventArgs e)
        { 
            if (dStartBox.SelectedDate != null && dEndBox.SelectedDate != null && dStartBox.SelectedDate <= dEndBox.SelectedDate)
            {
                var now = DateTime.Now;
                depList.ItemsSource = null;
                depList.ItemsSource = dep.DateDep(dStartBox.SelectedDate ?? now, dEndBox.SelectedDate ?? now, choiceDepo.SelectedItem as string);
            }
             else   
                MessageBox.Show("Please select a correct arbitrary period");
        }
        private void ResetDep_Click(object sender, RoutedEventArgs e)
        {
            dStartBox.SelectedDate = null;
            dEndBox.SelectedDate = null;
            UpdateDep(); 
        }
        private void MakeDep_Click(object sender, RoutedEventArgs e)
        {
            var winAddDep = new AddDeposit();
            winAddDep.UpdateDeposit += UpdateDep;
            winAddDep.UpdateDeposit += UpdateAccount;
            winAddDep.Show();
        }
        private void MakeLoan_Click(object sender, RoutedEventArgs e)//
        {
            var winAddLoan = new AddLoanWindow();
            winAddLoan.UpdateLoan += UpdateLoan;
            winAddLoan.UpdateLoan += UpdateAccount;
            winAddLoan.Show();
        }
        public void UpdateDep()//
        {
            depList.ItemsSource = null;
            dep = Factory.Instance.GDeposit();
            depList.ItemsSource = dep.Deposits;
        }
        public void UpdateLoan()//
        {
            loanList.ItemsSource = null;
            lo = Factory.Instance.GLoan();
            loanList.ItemsSource = lo.Loans;
        }
        private void RepayLoan_Click(object sender, RoutedEventArgs e)//
        {
            var cLo = loanList.SelectedItem as Loan;
            if (cLo == null)
                MessageBox.Show("Please select a loan");
            else if (cLo.Status == "Expired")
                MessageBox.Show("You cannot repay this loan, because it is expired");
            else if (cLo.Status == "Repaid")
                MessageBox.Show("This loan is already repaid");
            else if (cLo.StartDate.AddDays(180) > DateTime.Now)
                MessageBox.Show("The minimum loan period (180 days) isn't over, so you can't repay that deposit");
            else 
            {
                if (lo.CloseLoan(cLo))
                {
                    MessageBox.Show("Loan has been repaid");
                    UpdateLoan();
                    UpdateAccount();
                }
                else
                    MessageBox.Show("Account has insufficient funds");
            }
        }
        public void UpdateAccount()//
        {
            accountList.ItemsSource = null;
            acc = Factory.Instance.GAccount();
            accountList.ItemsSource = acc.Accounts;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)//
        {
            int accId;
            if (int.TryParse(SearchAccBox.Text, out accId))
                accountList.ItemsSource = acc.ClAcc(accId);
            else
                MessageBox.Show("Please, enter correct client id");
        }
        private void AddAccount_Click(object sender, RoutedEventArgs e)//
        {
            var accAddWin = new AddAccountWindow();
            accAddWin.UpdateAccount += UpdateAccount;
            accAddWin.Show();
        }
        private void ResetAcc_Click(object sender, RoutedEventArgs e)
        {
            SearchAccBox.Text = null;
            UpdateAccount();
        }
        private void ChangeAccountStatus_Click(object sender, RoutedEventArgs e)
        {
            var acd = accountList.SelectedItem as Account;
            if (acd != null)
            {
                acc.ChangeAccountStatus(acd);
                UpdateAccount();
                MessageBox.Show("Status has been changed successfully");
            }
            else
                MessageBox.Show("Please select an account");
        }
        public void UpdateTran()
        {
            tranList.ItemsSource = null;
            tr = Factory.Instance.GTransaction();
            tranList.ItemsSource = tr.Transactions;
        }
        private void ExecuteTran_Click(object sender, RoutedEventArgs e)
        {
            if (tStartBox.SelectedDate != null && tEndBox.SelectedDate != null && tStartBox.SelectedDate <= tEndBox.SelectedDate)
            {
                var now = DateTime.Now;
                tranList.ItemsSource = null;
                tranList.ItemsSource = tr.DateTran(tStartBox.SelectedDate ?? now, tEndBox.SelectedDate ?? now);
            }
            else
                MessageBox.Show("Please select a correct arbitrary period");
            
        }
        private void TranResetButton_Click(object sender, RoutedEventArgs e)
        {
            tStartBox.SelectedDate = null;
            tEndBox.SelectedDate = null;
            UpdateTran();
        }
        private void MakeDepo_Click(object sender, RoutedEventArgs e)
        {
            var winAddDep = new AddDeposit();
            winAddDep.UpdateDeposit += UpdateDep;
            winAddDep.UpdateDeposit += UpdateAccount;
            winAddDep.Show();
        }

        private void CloseDep_Click(object sender, RoutedEventArgs e)//kirill
        {
            var closeDe = depList.SelectedItem as Deposit;
            if (closeDe == null)
                MessageBox.Show("Please select a deposit");
            else if (closeDe.Status == "Closed")
                MessageBox.Show("This deposit is already closed");
            else if (closeDe.StartDate.AddDays(180) > DateTime.Now)
                MessageBox.Show("The minimum deposit period (180 days) isn't over, so you can't close that deposit");
            else
            {
                dep.CloseDeposit(closeDe);
                UpdateDep();
                MessageBox.Show("Deposit has been closed");
            }
        }
        private void AddTran_Click(object sender, RoutedEventArgs e)
        {
            var winAddTran = new MakeTransactionWindow();
            winAddTran.UpdateTran += UpdateTran;
            winAddTran.UpdateTran += UpdateAccount;
            winAddTran.Show();
        }
    }
}
