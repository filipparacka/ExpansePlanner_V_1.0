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
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (dl.ValidateUser(textBox.Text,passwordBox.Password.ToString()))
            {
                MessageBox.Show("Connection succesful");
                Global.UserID = dl.GetUserID(textBox.Text);
                Global.Username = textBox.Text;
            }
        }
    }
}
