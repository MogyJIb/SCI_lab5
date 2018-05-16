using DbDataLibrary.Data;
using DbDataLibrary.Models;
using SCI_lab5.Extensions;
using SCI_lab5.Models.Sorts;
using SCI_lab5.Models.Filters;
using SCI_lab5.Utils;
using SCI_lab5.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SCI_lab5.Extensions.Filters;
using System;

namespace SCI_lab5.Controllers
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
        public IActionResult Index(int pageNumber = 1)
        {
            TourKindViewModel viewModel = new TourKindViewModel();

            var sessionFilter = HttpContext.Session.Get(Constants.TourKindFilter);
            if (sessionFilter != null)
                viewModel.TourKindFilter = Converter.DictionaryToObject<TourKindFilter>(sessionFilter);
            var sessionSortState = HttpContext.Session.Get(Constants.TourKindSort);
            if (sessionSortState != null && sessionSortState.Count > 0)
            {
                TourKindSort.State currSortState = (TourKindSort.State)Enum.Parse(typeof(TourKindSort.State), sessionSortState["sortState"]);
                viewModel.TourKindSort = new TourKindSort(currSortState);
            }


            HttpContext.Session.Set<int>(Constants.ClientPageNumber, pageNumber);
            SetTourKinds(viewModel, pageNumber);

            return View(viewModel);
        }


        [HttpGet]
        [SetToSession(Constants.TourKindSort)]
        public IActionResult Sort(TourKindSort.State sortState = TourKindSort.State.NoSort)
        {
            TourKindViewModel viewModel = new TourKindViewModel();

            var sessionFilter = HttpContext.Session.Get(Constants.TourKindFilter);
            if (sessionFilter != null)
                viewModel.TourKindFilter = Converter.DictionaryToObject<TourKindFilter>(sessionFilter);
            viewModel.TourKindSort = new TourKindSort(sortState);

            int pageNumber = HttpContext.Session.Get<int>(Constants.ClientPageNumber);
            if (pageNumber < 1) pageNumber = 1;
            SetTourKinds(viewModel, pageNumber);

            return View("Index",viewModel);
        }

        [HttpPost]
        [SetToSession(Constants.TourKindFilter)]
        public IActionResult Filter(TourKindFilter tourKindFilter)
        {
            TourKindViewModel viewModel = new TourKindViewModel();

            var sessionSortState = HttpContext.Session.Get(Constants.TourKindSort);
            if (sessionSortState != null && sessionSortState.Count > 0)
            {
                TourKindSort.State currSortState = (TourKindSort.State)Enum.Parse(typeof(TourKindSort.State), sessionSortState["sortState"]);
                viewModel.TourKindSort = new TourKindSort(currSortState);
            }
            viewModel.TourKindFilter = tourKindFilter;
            int pageNumber = HttpContext.Session.Get<int>(Constants.ClientPageNumber);
            if (pageNumber < 1) pageNumber = 1;
            SetTourKinds(viewModel, pageNumber);

            return View("Index", viewModel);
        }

        private void SetTourKinds(TourKindViewModel viewModel, int pageNumber)
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

            int pageSize = 10;
            viewModel.PageViewModel = new PageViewModel(tourkinds.Count(), pageNumber, pageSize);
            tourkinds = tourkinds.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            viewModel.TourKinds = tourkinds;
        }
    }
}
