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
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        IClient cl = Factory.Instance.GClient();
        Client clien;
        public event Action UpdateClient;
        public EditClientWindow(Client cd)
        {
            InitializeComponent();
            clien = cd;
            SetInfo();
        }
        public void SetInfo()
        {
            nameBox.Text = clien.Name;
            addressBox.Text = clien.Address;
            phoneBox.Text = clien.Phone;
            emailBox.Text = clien.Email;
            birthBox.SelectedDate = clien.BirthDate;
            idBox.Text += $" {clien.Id.ToString()}";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var phone = phoneBox.Text;
            var name = nameBox.Text;
            var email = emailBox.Text;
            var address = addressBox.Text;
            if (birthBox.SelectedDate != null && phone.All(char.IsDigit))
            {
                DateTime date = birthBox.SelectedDate ?? DateTime.Now;
                cl.EditClient(name, date, email, phone, address, clien.Id);
                UpdateClient?.Invoke();
                Close();
                MessageBox.Show("Changes have been saved");
            }
            else
                MessageBox.Show("Invalid input data");
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            SetInfo();
        }
    }
}
