using DbDataLibrary.Models;
using lab4.Models.Filters;
using lab4.Models.Sorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.ViewModels
{
    public class TourKindViewModel
    {
        public IEnumerable<TourKind> TourKinds { get; set; }
        public TourKindFilter TourKindFilter { get; set; }
        public TourKindSort TourKindSort { get; set; }

        public TourKindViewModel()
        {
            this.TourKindFilter = new TourKindFilter();
            this.TourKindSort = new TourKindSort();
        }
    }
}
