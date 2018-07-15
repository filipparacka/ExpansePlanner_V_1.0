using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    public class ViewModel
    {

        DomainLogic dl = new DomainLogic();
        ExpenseBO expense = new ExpenseBO();

        public string Username { get; set; }

        public IEnumerable<string> ExpenseCategories
        {
            get
            {
                return dl.GetExpenseCategories();
            }
            set
            {
                value = dl.GetExpenseCategories();
            }
        }

        public IEnumerable<string> IncomeCategories
        {
            get
            {
                return dl.GetIncomeCategories();
            }
            set
            {
                value = dl.GetIncomeCategories();
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
            Username = Global.Username;
        }
    }
}
