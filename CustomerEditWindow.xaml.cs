using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CustomerEditWindow.xaml
    /// </summary>
    public partial class CustomerEditWindow : Window
    {
        public Customer Customer { get; private set; }

        public CustomerEditWindow(Customer customer)
        {
            InitializeComponent();
            Customer = customer;
            DataContext = Customer;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Customer.FullName))
            {
                MessageBox.Show("Пожалуйста, введите ФИО клиента", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
