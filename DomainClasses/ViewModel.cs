﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    public class ViewModel
    {

        public string Username { get; set; }

        public ViewModel()
        {
            Username = Global.Username;
        }
    }
}
