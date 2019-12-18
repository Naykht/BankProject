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
    /// Логика взаимодействия для MoreClientWindow.xaml
    /// </summary>
    public partial class MoreClientWindow : Window
    {
        Client cl;
        IAccount acc = Factory.Instance.GAccount();
        public MoreClientWindow(Client cd)
        {
            InitializeComponent();
            cl = cd;
            UpdateInfo();
        }
        public void UpdateInfo()
        {
            nBox.Text = cl.Name;
            bBox.Text = cl.BirthDate.ToString();
            eBox.Text = cl.Email;
            pBox.Text = cl.Phone;
            aBox.Text = cl.Address;
            accList.ItemsSource = null;
            accList.ItemsSource = acc.ClAcc(cl.Id) ;
        }

        private void Loan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Dep_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tran_Click(object sender, RoutedEventArgs e)
        {
            var acc = accList.SelectedItem as Account;
            if (acc != null)
            {
                var tranwin = new TransactionWindow(acc);
                tranwin.Show();
            }
                
        }
    }
}
