using DbDataLibrary.Models;
using SCI_lab5.Models.Filters;
using SCI_lab5.Models.Sorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCI_lab5.ViewModels
{
    public class TourKindViewModel
    {
        public IEnumerable<TourKind> TourKinds { get; set; }
        public TourKindFilter TourKindFilter { get; set; }
        public TourKindSort TourKindSort { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public TourKindViewModel()
        {
            this.TourKindFilter = new TourKindFilter();
            this.TourKindSort = new TourKindSort();
        }
    }
}
