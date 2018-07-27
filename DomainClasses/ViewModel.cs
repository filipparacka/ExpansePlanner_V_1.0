using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace DomainClasses
{
    public class ViewModel:INotifyPropertyChanged
    {
        #region Fields
        public ICommand AddExpenseCategoryCommand { get; set; }

        public ICommand AddIncomeCategoryCommand { get; set; }

        private DomainLogic dl = new DomainLogic();

        private ExpenseBO expense = new ExpenseBO();

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> expenseCategories = new ObservableCollection<string>();

        private ObservableCollection<string> incomeCategories = new ObservableCollection<string>();

        private string Username { get; set; }

        private string _expenseCategoryName;

        private string _incomeCategoryName;
        #endregion

        public string ExpenseCategoryName
        {
            get { return _expenseCategoryName; }
            set { _expenseCategoryName = value; OnPropertyChanged("ExpenseCategoryName"); }
        }

        public string IncomeCategoryName
        {
            get { return _incomeCategoryName; }
            set { _incomeCategoryName = value; OnPropertyChanged("IncomeCategoryName"); }
        }

        public ObservableCollection<string> ExpenseCategories
        {
            get
            {
                return expenseCategories;
            }
            set
            {
                expenseCategories = value;
                OnPropertyChanged("ExpenseCategories");
            }
        }

        public ObservableCollection<string> IncomeCategories
        {
            get
            {
                return incomeCategories;
            }
            set
            {
                incomeCategories = value;
                OnPropertyChanged("IncomeCategories");
            }
        }

        public IEnumerable<ExpenseBO> AllExpenses
        {
            get
            {
                return dl.GetAllExpenses();
            }
        }

        public ViewModel()
        {
            AddExpenseCategoryCommand = new Command(ExecuteMethod, CanExecuteMethod);
            AddIncomeCategoryCommand = new Command(ExecuteAddIncomeCategoryCommand,CanExecuteMethod);
            expenseCategories = new ObservableCollection<string>(dl.GetExpenseCategories());
            incomeCategories = new ObservableCollection<string>(dl.GetIncomeCategories());
            Username = Global.Username;
        }

        private void ExecuteAddIncomeCategoryCommand(object obj)
        {
            dl.CreateNewIncomeCategory(IncomeCategoryName);
            IncomeCategories = new ObservableCollection<string>(dl.GetIncomeCategories());
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        private void ExecuteMethod(object obj)
        {
            dl.CreateNewExpenseCategory(ExpenseCategoryName);
            ExpenseCategories = new ObservableCollection<string>(dl.GetExpenseCategories());
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
     
    }
}
