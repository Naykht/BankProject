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
            bBox.Text = cl.BirthDate.ToString("dd/MM/yyyy");
            eBox.Text = cl.Email;
            pBox.Text = cl.Phone;
            aBox.Text = cl.Address;
            accList.ItemsSource = null;
            accList.ItemsSource = acc.ClAcc(cl.Id) ;
        }


        private void Operations_Click(object sender, RoutedEventArgs e)
        {
            var acd = accList.SelectedItem as Account;
            if (acc != null)
            {
                var tranwin = new AccountOperationWindow(acd);
                tranwin.Show();
            }
        }
    }
}
