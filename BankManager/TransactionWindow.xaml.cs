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
    /// Логика взаимодействия для TransactionWindow.xaml
    /// </summary>
    public partial class TransactionWindow : Window
    {
        Account acc;
        ITransaction tr = Factory.Instance.GTransaction();
        public TransactionWindow(Account acd)
        {
            InitializeComponent();
            acc = acd;
            Update();
        }
        public void Update()
        {
            AccId.Text += $" {acc.AccId}";
            TranData.ItemsSource = null;
            TranData.ItemsSource = tr.AccTran(acc.AccId);
        }
    }
}
