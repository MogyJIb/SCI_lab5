using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.Models.Filters
{
    public class ClientFilter
    {
        public string Name { get; set; }
        public string Birthyear { get; set; }

        public ClientFilter()
        {
            Birthyear = ""; Name = "";
        }
    }
}
