using System;
using System.Collections.Generic;
using System.Globalization;
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Categories cat = new Categories();
            cat.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            dl.CreateNewExpense(textBox1.Text, (DateTime)datepick.SelectedDate, ExpCategory.Text, Convert.ToDecimal(textBox2.Text));
            MessageBox.Show("New Expense Added!");
            dataGrid.ItemsSource = dl.GetAllExpenses();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dl.SumMonthlyExpensesAndIncomes();
        }

        private void addIncome_Click(object sender, RoutedEventArgs e)
        {
            dl.CreateNewIncome(incomeDesc.Text,(DateTime)incomeDate.SelectedDate,categoryCmb.Text,Convert.ToDecimal(incomeCost.Text));
            MessageBox.Show("New Income Added!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void bla_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            SelectCulture("sk-SK");
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectCulture("en-EN");
        }

        public static void SelectCulture(string culture)
        {
            if (String.IsNullOrEmpty(culture))
                return;

            //Copy all MergedDictionarys into a auxiliar list.
            var dictionaryList = Application.Current.Resources.MergedDictionaries.ToList();

            //Search for the specified culture.     
            string requestedCulture = string.Format("StringResources.{0}.xaml", culture);
            var resourceDictionary = dictionaryList.
                FirstOrDefault(d => d.Source.OriginalString == requestedCulture);

            if (resourceDictionary == null)
            {
                //If not found, select our default language.             
                requestedCulture = "StringResources.en-EN.xaml";
                resourceDictionary = dictionaryList.
                    FirstOrDefault(d => d.Source.OriginalString == requestedCulture);
            }

            //If we have the requested resource, remove it from the list and place at the end.     
            //Then this language will be our string table to use.      
            if (resourceDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            //Inform the threads of the new culture.     
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

        }
    }
}
