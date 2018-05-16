using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.Models.Filters
{
    public class TourFilter
    {
        public string Name { get; set; }

        public string ClientName { get; set; }
        public string TourKindName { get; set; }

        public TourFilter()
        {
            Name = "";
            ClientName = "";
            TourKindName = "";
        }
    }
}
