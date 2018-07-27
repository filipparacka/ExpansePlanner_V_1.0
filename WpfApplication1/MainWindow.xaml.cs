using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DomainClasses;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DomainLogic dl = new DomainLogic();
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string name = textBox.Text;
            string password = passwordBox.Password.ToString();
            grid.IsEnabled = false;
            Loading.Visibility = Visibility.Visible;
            if (await dl.ValidateUser(name, password))
            {
                Global.UserID = dl.GetUserID(textBox.Text);
                Global.Username = textBox.Text;
                Window1 win = new Window1();
                win.Show();
                this.Close();
            }
            else
            {
                Loading.Visibility = Visibility.Collapsed;
                grid.IsEnabled = true;
                MessageBox.Show("Wrong Username or password!");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NewUser win = new NewUser();
            win.Show();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button_Click(this, null);
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StaticClass.SelectCulture("en-EN");
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            StaticClass.SelectCulture("sk-SK");
        }
    }
}
