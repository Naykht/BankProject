using banks;
using banks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        
        public event Action UpdateClient;
        string phone;
        string name;
        string email;
        string address;
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            phone = phoneBox.Text;
            name = nameBox.Text;
            email = emailBox.Text;
            address = addressBox.Text;
            if (birthBox.SelectedDate != null && phone.All(char.IsDigit))
            {
                IClient cl = Factory.Instance.GClient();
                DateTime date = birthBox.SelectedDate ?? DateTime.Now;
                cl.AddClient(name, date, email, phone, address);
                UpdateClient?.Invoke();
                MessageBox.Show("You successfully added new client");
                phoneBox.Text = null;
                nameBox.Text = null;
                emailBox.Text = null;
                addressBox.Text = null;
                birthBox.Text = null;
            }
            else
                MessageBox.Show("Invalid input data");
        }
    }
}
