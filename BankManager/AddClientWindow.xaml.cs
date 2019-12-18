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
        IClient cl = Factory.Instance.GClient();
        public event Action<List<Client>> UpdateClient;
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var phone = phoneBox.Text;
            var name = nameBox.Text;
            var email = emailBox.Text;
            var address = addressBox.Text;
            
            if (birthBox.SelectedDate != null && phone.All(char.IsDigit))
            {
                DateTime date = birthBox.SelectedDate ?? DateTime.Now;
                cl.AddClient(name, date, email, phone, address);
                UpdateClient?.Invoke(cl.Clients);
                Close();
                MessageBox.Show("You successfully added new client");
                

            }
            else
                MessageBox.Show("Invalid input data");
        }
    }
}
