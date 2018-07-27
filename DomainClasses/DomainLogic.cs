using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Threading;

namespace DomainClasses
{
    public class DomainLogic
    {
        public async Task<bool> ValidateUser(string name, string password)
        {
            using (var context = new ExpenseEntities())
            {
                var result = await Task.Run(() => context.Users.AnyAsync(x => x.Name == name && x.Password == password));
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

        public void CreateNewExpenseCategory(string name)
        {
            using (var context = new ExpenseEntities())
            {
                ExpenseCategory category = new ExpenseCategory() { CategoryName = name};
                context.ExpenseCategory.Add(category);
                context.SaveChanges();
            }
        }

        public void CreateNewIncomeCategory(string name)
        {
            using (var context = new ExpenseEntities())
            {
                IncomeCategory category = new IncomeCategory() { CategoryName = name };
                context.Incomecategory.Add(category);
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

        public void CreateNewIncome(string description, DateTime date, string category, decimal price)
        {
            using (var context = new ExpenseEntities())
            {
                IncomeCategory cat = context.Incomecategory.Where(x => x.CategoryName == category).FirstOrDefault();
                Incomes income = new Incomes() { Description = description, Date = date, Price = price, Category = cat, Users_ID = Global.UserID };
                context.Incomes.Add(income);
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

        public List<ExpenseAndIncome> SumMonthlyExpensesAndIncomes()
        {
            using (var context = new ExpenseEntities())
            {
                var expenses = context.Expenses
                    .GroupBy(g => new { g.Date.Month, g.Date.Year })
                    .Select(g => new  { MonthAndYear = g.Key.Month + "/" + g.Key.Year, Expenses = g.Sum(x => x.Price) }).ToList();

                var incomes = context.Incomes
                    .GroupBy(g => new { g.Date.Month, g.Date.Year })
                    .Select(g => new  { MonthAndYear = g.Key.Month + "/" + g.Key.Year, Incomes = g.Sum(x => x.Price) }).ToList();

                var query = expenses.GroupJoin(incomes, e => e.MonthAndYear, i => i.MonthAndYear, (e, g) => g.Select(i => new ExpenseAndIncome { MonthAndYear = e.MonthAndYear, Expenses = e.Expenses, Incomes = i.Incomes })
                    .DefaultIfEmpty(new ExpenseAndIncome { MonthAndYear = e.MonthAndYear, Expenses = e.Expenses, Incomes = null  }))
                    .SelectMany(g => g);

                var query2 = incomes.GroupJoin(expenses, i => i.MonthAndYear, e => e.MonthAndYear, (i, g) => g.Select(e => new ExpenseAndIncome { MonthAndYear = i.MonthAndYear, Expenses = e.Expenses, Incomes = i.Incomes })
                    .DefaultIfEmpty(new ExpenseAndIncome { MonthAndYear = i.MonthAndYear, Expenses = null, Incomes = i.Incomes }))
                    .SelectMany(g => g);

                return query.Union(query2).GroupBy(x => x.MonthAndYear).Select(g => g.First()).ToList();
            }
        }
    }

    public class ExpenseBO
    {
        public string Month { get; set; }
        public string Category { get; set; }
        public string Desc { get; set; }
        public DateTime Date { get; set; }
        public decimal? Price { get; set; }
    }

    public class ExpenseAndIncome
    {
        public string MonthAndYear { get; set; }
        public decimal? Expenses { get; set; }
        public decimal? Incomes { get; set; }
    }
}
