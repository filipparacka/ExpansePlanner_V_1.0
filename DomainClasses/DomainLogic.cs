using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    public class DomainLogic
    {
        public bool ValidateUser(string name, string password)
        {
            using (var context = new Entities())
            {
                var result = context.USERS.Where(x => x.NAME == name && x.PASSWORD == password).Any();
                return result;
            }
        }

        public decimal GetUserID(string name)
        {
            using (var context = new Entities())
            {
                var result = context.USERS.Where(x => x.NAME == name).Select(x => x.ID).FirstOrDefault();
                return result;
            }
        }
    }
}
