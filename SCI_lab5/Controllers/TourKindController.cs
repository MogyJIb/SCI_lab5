using DbDataLibrary.Data;
using DbDataLibrary.Models;
using lab4.Extensions;
using lab4.Models.Sorts;
using lab4.Models.Filters;
using lab4.Utils;
using lab4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using lab4.Extensions.Filters;
using System;

namespace lab4.Controllers
{
    [ExceptionFilter]
    [TypeFilter(typeof(LogFilter))]
    public class TourKindController : Controller
    {
        private ToursSqliteDbContext _db;

        public TourKindController(ToursSqliteDbContext dbContext)
        {
            _db = dbContext;
        }

        [HttpGet]
        [SetToSession(Constants.TourKindSort)]
        public IActionResult Index(TourKindSort.State sortState = TourKindSort.State.NoSort)
        {
            TourKindViewModel viewModel = new TourKindViewModel();

            var sessionFilter = HttpContext.Session.Get(Constants.TourKindFilter);
            if (sessionFilter != null)
                viewModel.TourKindFilter = Converter.DictionaryToObject<TourKindFilter>(sessionFilter);
            viewModel.TourKindSort = new TourKindSort(sortState);

            SetTourKinds(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [SetToSession(Constants.TourKindFilter)]
        public IActionResult Index(TourKindFilter tourKindFilter)
        {
            TourKindViewModel viewModel = new TourKindViewModel();

            var sessionSortState = HttpContext.Session.Get(Constants.TourKindSort);
            if (sessionSortState != null && sessionSortState.Count > 0)
            {
                TourKindSort.State currSortState = (TourKindSort.State)Enum.Parse(typeof(TourKindSort.State), sessionSortState["sortState"]);
                viewModel.TourKindSort = new TourKindSort(currSortState);
            }
            viewModel.TourKindFilter = tourKindFilter;

            SetTourKinds(viewModel);

            return View(viewModel);
        }

        private void SetTourKinds(TourKindViewModel viewModel)
        {
            var tourkinds = _db.TourKinds.ToList();
            switch (viewModel.TourKindSort.Models.CurrentState)
            {
                case TourKindSort.State.NameAsc:
                    tourkinds = tourkinds.OrderBy(t => t.Name).ToList();
                    break;
                case TourKindSort.State.NameDesc:
                    tourkinds = tourkinds.OrderByDescending(t => t.Name).ToList();
                    break;

                case TourKindSort.State.DescriptionAsc:
                    tourkinds = tourkinds.OrderBy(t => t.Description).ToList();
                    break;
                case TourKindSort.State.DescriptionDesc:
                    tourkinds = tourkinds.OrderByDescending(t => t.Description).ToList();
                    break;

                case TourKindSort.State.ConstraintsAsc:
                    tourkinds = tourkinds.OrderBy(t => t.Constraints).ToList();
                    break;
                case TourKindSort.State.ConstraintsDesc:
                    tourkinds = tourkinds.OrderByDescending(t => t.Constraints).ToList();
                    break;
        }

           
            string name = viewModel.TourKindFilter.Name;
            if (name == null)
                viewModel.TourKindFilter.Name = "";
            else if (name != "")
                tourkinds = tourkinds.Where(t => t.Name.Contains(name)).ToList();
            
            viewModel.TourKinds = tourkinds;
        }
    }
}
