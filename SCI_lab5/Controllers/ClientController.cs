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

    [TypeFilter(typeof(LogFilter))]
    [ExceptionFilter]
    public class ClientController: Controller
    {
        private ToursSqliteDbContext _db;

        public ClientController(ToursSqliteDbContext dbContext)
        {
            _db = dbContext;
        }

        [HttpGet]
        [SetToSession(Constants.ClientSort)]
        public IActionResult Index(ClientSort.State sortState = ClientSort.State.NoSort)
        {
            ClientViewModel viewModel = new ClientViewModel();
          
            var sessionFilter = HttpContext.Session.Get(Constants.ClientFilter);
            if (sessionFilter != null)
                 viewModel.ClientFIlter = Converter.DictionaryToObject<ClientFilter>(sessionFilter);
            viewModel.ClientSort = new ClientSort(sortState);

            SetClients(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [SetToSession(Constants.ClientFilter)]
        public IActionResult Index(ClientFilter clientFilter)
        {
            ClientViewModel viewModel = new ClientViewModel();

            var sessionSortState = HttpContext.Session.Get(Constants.ClientSort);
            if (sessionSortState != null && sessionSortState.Count > 0)
            {
                ClientSort.State currSortState = (ClientSort.State)Enum.Parse(typeof(ClientSort.State), sessionSortState["sortState"]);
                viewModel.ClientSort = new ClientSort(currSortState);
            }
            viewModel.ClientFIlter = clientFilter;

            SetClients(viewModel);

            return View(viewModel);
        }

        private void SetClients(ClientViewModel viewModel)
        {
            var clients = _db.Clients.ToList();
            switch (viewModel.ClientSort.Models.CurrentState)
            {
                case ClientSort.State.NameAsc:
                    clients = clients.OrderBy(t => t.Name).ToList();
                    break;
                case ClientSort.State.NameDesc:
                    clients = clients.OrderByDescending(t => t.Name).ToList();
                    break;

                case ClientSort.State.PhoneAsc:
                    clients = clients.OrderBy(t => t.Phone).ToList();
                    break;
                case ClientSort.State.PhoneDesc:
                    clients = clients.OrderByDescending(t => t.Phone).ToList();
                    break;

                case ClientSort.State.BirthdayAsc:
                    clients = clients.OrderBy(t => t.Birthday).ToList();
                    break;
                case ClientSort.State.BirthdayDesc:
                    clients = clients.OrderByDescending(t => t.Birthday).ToList();
                    break;

                case ClientSort.State.AddressAsc:
                    clients = clients.OrderBy(t => t.Address).ToList();
                    break;
                case ClientSort.State.AddressDesc:
                    clients = clients.OrderByDescending(t => t.Address).ToList();
                    break;
            }

            try
            {
                string sBirthYear = viewModel.ClientFIlter.Birthyear;
                if (sBirthYear == null) viewModel.ClientFIlter.Birthyear = "";

                int birthYear = Int32.Parse(sBirthYear);
                clients = clients.Where(t => t.Birthday.Year == birthYear).ToList();
            }catch(Exception ex) { }

            string name = viewModel.ClientFIlter.Name;
            if (name == null)
                viewModel.ClientFIlter.Name = "";
            else if (name != "")
                clients = clients.Where(t => t.Name.Contains(name)).ToList();

            viewModel.Clients = clients;
        }
    }
}
