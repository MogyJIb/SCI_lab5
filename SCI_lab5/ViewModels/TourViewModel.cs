using DbDataLibrary.Models;
using lab4.Models.Filters;
using lab4.Models.Sorts;
using System.Collections.Generic;

namespace lab4.ViewModels
{
    public class TourViewModel
    {
        public IEnumerable<Tour> Tours { get; set; }
        public TourFilter TourFilter { get; set; }
        public TourSort TourSort { get; set; }

        public TourViewModel()
        {
            this.TourFilter = new TourFilter();
            this.TourSort = new TourSort();
        }
    }
}
