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
    /// Логика взаимодействия для AccountOperationWindow.xaml
    /// </summary>
    public partial class AccountOperationWindow : Window
    {
        Account acc;
        ITransaction tr = Factory.Instance.GTransaction();
        IDeposit dep = Factory.Instance.GDeposit();
        ILoan lo = Factory.Instance.GLoan();
        public AccountOperationWindow(Account acd)
        {
            InitializeComponent();
            acc = acd;
            Title = $"Account Id - {acc.AccId.ToString()}";
            Update();
        }
        public void Update()
        {
            TranData.ItemsSource = null;
            TranData.ItemsSource = tr.AccTran(acc.AccId);
            LoanData.ItemsSource = null;
            LoanData.ItemsSource = lo.AccLoan(acc.AccId);
            DepData.ItemsSource = null;
            DepData.ItemsSource = dep.AccDep(acc.AccId);
        }
    }
}
