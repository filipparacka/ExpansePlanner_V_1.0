using DomainClasses;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        DomainLogic dl = new DomainLogic();
        public NewUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!dl.UserExists(textBox.Text))
            {
                if (passwordBox.Password == passwordBox1.Password)
                {
                    dl.CreateNewUser(textBox.Text, passwordBox.Password);
                    MessageBox.Show("User successfully created", "User created", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter valid password", "Invalid password", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Username already exists! Please enter different name.", "Invalid username", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
