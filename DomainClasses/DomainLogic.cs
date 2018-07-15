using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DomainClasses
{
    public class DomainLogic
    {
        public bool ValidateUser(string name, string password)
        {
            using (var context = new ExpenseEntities())
            {
                var result = context.Users.Any(x => x.Name == name && x.Password == password);
                return result;
            }
        }

        public int GetUserID(string name)
        {
            using (var context = new ExpenseEntities())
            {
                var result = context.Users.Where(x => x.Name == name).Select(x => x.Id).FirstOrDefault();
                return Convert.ToInt32(result);
            }
        }

        public void CreateNewUser(string name, string password)
        {
           
            using (var context = new ExpenseEntities())
            {
                Users user = new Users() { Name = name, Password = password };
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public bool UserExists(string name)
        {
            using (var context = new ExpenseEntities())
            {
                var result = context.Users.Any(x => x.Name == name);
                return result;
            }
        }

        public IEnumerable<string> GetExpenseCategories()
        {
            using (var context = new ExpenseEntities())
            {
                var result = context.ExpenseCategory.Select(x => x.CategoryName).ToList();
                return result;
            }
        }

        public IEnumerable<string> GetIncomeCategories()
        {
            using (var context = new ExpenseEntities())
            {
                var result = context.Incomecategory.Select(x => x.CategoryName).ToList();
                return result;
            }
        }

        public void CreateNewCategory(string name)
        {
            using (var context = new ExpenseEntities())
            {
                ExpenseCategory category = new ExpenseCategory() { CategoryName = name};
                context.ExpenseCategory.Add(category);
                context.SaveChanges();
            }
        }

        public void CreateNewExpense(string description, DateTime date, string category, decimal price)
        {
            using (var context = new ExpenseEntities() )
            {
                ExpenseCategory cat = context.ExpenseCategory.Where(x => x.CategoryName == category).FirstOrDefault();
                Expenses expense = new Expenses() { Description = description, Date = date, Price = price, Category = cat , Users_ID = Global.UserID};
                context.Expenses.Add(expense);
                context.SaveChanges();
            }
        }

        public List<ExpenseBO> GetAllExpenses()
        {
            using (var context = new ExpenseEntities())
            {
                var result = context.Expenses.Select(x => new ExpenseBO {
                    Category = x.Category.CategoryName, Desc = x.Description, Date = x.Date, Price = x.Price }).ToList();
                return result;
            }
        }

        public List<ExpenseBO> SumMonthlyExpanses()
        {
            using (var context = new ExpenseEntities())
            {
                var result = context.Expenses
                    .GroupBy(g => new { g.Date.Month, g.Date.Year })
                    .Select(g => new ExpenseBO { Month = g.Key.Month, Price = g.Sum(x => x.Price) }).ToList();
                return result;
            }
        }
    }

    public class ExpenseBO
    {
        public int Month { get; set; }
        public string Category { get; set; }
        public string Desc { get; set; }
        public DateTime Date { get; set; }
        public decimal? Price { get; set; }
    }
}
