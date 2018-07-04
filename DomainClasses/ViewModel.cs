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

        public string Username { get; set; }

        public IEnumerable<string> Categories
        {
            get
            {
                return dl.GetCategories();
            }
            set
            {
                value = dl.GetCategories();
            }
        }

        public ViewModel()
        {
            Username = Global.Username;
        }
    }
}
