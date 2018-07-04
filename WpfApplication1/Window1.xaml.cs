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
using DomainClasses;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DomainLogic dl = new DomainLogic();
        public Window1()
        {
            InitializeComponent();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Categories cat = new Categories();
            cat.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                dl.CreateNewCategory(textBox.Text);
            }
            else
            {
                MessageBox.Show("Enter category!");
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            //dl.CreateNewExpense(textBox1.Text,datepick.DisplayDate,)
        }

        
    }
}
