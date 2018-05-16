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
using Microsoft.EntityFrameworkCore;

namespace lab4.Controllers
{
    [ExceptionFilter]
    [TypeFilter(typeof(LogFilter))]
    public class TourController : Controller
    {
        private ToursSqliteDbContext _db;

        public TourController(ToursSqliteDbContext dbContext)
        {
            _db = dbContext;
        }

        [HttpGet]
        [SetToSession(Constants.TourSort)]
        public IActionResult Index(TourSort.State sortState = TourSort.State.NoSort)
        {
            TourViewModel viewModel = new TourViewModel();

            var sessionFilter = HttpContext.Session.Get(Constants.TourFilter);
            if (sessionFilter != null)
                viewModel.TourFilter = Converter.DictionaryToObject<TourFilter>(sessionFilter);
            viewModel.TourSort = new TourSort(sortState);

            SetTours(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [SetToSession(Constants.TourFilter)]
        public IActionResult Index(TourFilter tourFilter)
        {
            TourViewModel viewModel = new TourViewModel();

            var sessionSortState = HttpContext.Session.Get(Constants.TourSort);
            if (sessionSortState != null && sessionSortState.Count > 0)
            {
                TourSort.State currSortState = (TourSort.State)Enum.Parse(typeof(TourSort.State), sessionSortState["sortState"]);
                viewModel.TourSort = new TourSort(currSortState);
            }
            viewModel.TourFilter = tourFilter;

            SetTours(viewModel);

            return View(viewModel);
        }

        private void SetTours(TourViewModel viewModel)
        {
            var tours = _db.Tours.Include(t=> t.Client).Include(t => t.TourKind).ToList();
            switch (viewModel.TourSort.Models.CurrentState)
            {
                case TourSort.State.NameAsc:
                    tours = tours.OrderBy(t => t.Name).ToList();
                    break;
                case TourSort.State.NameDesc:
                    tours = tours.OrderByDescending(t => t.Name).ToList();
                    break;

                case TourSort.State.PriceAsc:
                    tours = tours.OrderBy(t => t.Price).ToList();
                    break;
                case TourSort.State.PriceDesc:
                    tours = tours.OrderByDescending(t => t.Price).ToList();
                    break;

                case TourSort.State.StartDateAsc:
                    tours = tours.OrderBy(t => t.StartDate).ToList();
                    break;
                case TourSort.State.StartDateDesc:
                    tours = tours.OrderByDescending(t => t.StartDate).ToList();
                    break;

                case TourSort.State.EndDateAsc:
                    tours = tours.OrderBy(t => t.EndDate).ToList();
                    break;
                case TourSort.State.EndDateDesc:
                    tours = tours.OrderByDescending(t => t.EndDate).ToList();
                    break;

                case TourSort.State.ClientAsc:
                    tours = tours.OrderBy(t => t.Client.Name).ToList();
                    break;
                case TourSort.State.ClientDesc:
                    tours = tours.OrderByDescending(t => t.Client.Name).ToList();
                    break;

                case TourSort.State.TourKindAsc:
                    tours = tours.OrderBy(t => t.TourKind.Name).ToList();
                    break;
                case TourSort.State.TourKindDesc:
                    tours = tours.OrderByDescending(t => t.TourKind.Name).ToList();
                    break;
            }

            string name = viewModel.TourFilter.Name;
            if (name == null)
                viewModel.TourFilter.Name = "";
            else if (name != "")
                tours = tours.Where(t => t.Name.Contains(name)).ToList();

            string clientName = viewModel.TourFilter.ClientName;
            if (clientName == null)
                viewModel.TourFilter.ClientName = "";
            else if (clientName != "")
                tours = tours.Where(t => t.Client.Name.Contains(clientName)).ToList();

            string tourKindName = viewModel.TourFilter.TourKindName;
            if (tourKindName == null)
                viewModel.TourFilter.TourKindName = "";
            else if (tourKindName != "")
                tours = tours.Where(t => t.TourKind.Name.Contains(tourKindName)).ToList();

            viewModel.Tours = tours;
        }
    }
}
