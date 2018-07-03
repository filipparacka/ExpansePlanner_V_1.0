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
                var result = context.USERS.Any(x => x.NAME == name && x.PASSWORD == password);
                return result;
            }
        }

        public int GetUserID(string name)
        {
            using (var context = new ExpenseEntities())
            {
                var result = context.USERS.Where(x => x.NAME == name).Select(x => x.ID).FirstOrDefault();
                return Convert.ToInt32(result);
            }
        }

        public void CreateNewUser(string name, string password)
        {
           
            using (var context = new ExpenseEntities())
            {
                USERS user = new USERS() { NAME = name, PASSWORD = password };
                context.USERS.Add(user);
                context.SaveChanges();
            }
        }

        public bool UserExists(string name)
        {
            using (var context = new ExpenseEntities())
            {
                var result = context.USERS.Any(x => x.NAME == name);
                return result;
            }
        }
    }
}
