using DbDataLibrary.Models;
using SCI_lab5.Models.Filters;
using SCI_lab5.Models.Sorts;
using System.Collections.Generic;

namespace SCI_lab5.ViewModels
{
    public class TourViewModel
    {
        public IEnumerable<Tour> Tours { get; set; }
        public TourFilter TourFilter { get; set; }
        public TourSort TourSort { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public TourViewModel()
        {
            this.TourFilter = new TourFilter();
            this.TourSort = new TourSort();
        }
    }
}
