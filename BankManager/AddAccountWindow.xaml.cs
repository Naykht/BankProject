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
    /// Логика взаимодействия для AddAccountWindow.xaml
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        IAccount ac = Factory.Instance.GAccount();
        IClient cl = Factory.Instance.GClient();
        public event Action UpdateAccount;
        public AddAccountWindow()
        {
            InitializeComponent();
            clientCombo.ItemsSource = cl.Clients;   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var startBalance = Convert.ToDecimal(balanceBox.Text);
            var chosenClientId = (clientCombo.SelectedItem as Client).Id ;

            ac.AddAccount(chosenClientId, startBalance);
            UpdateAccount?.Invoke();
            Close();

        }
    }
}
