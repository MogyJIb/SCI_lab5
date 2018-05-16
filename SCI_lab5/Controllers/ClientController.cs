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
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace SCI_lab5.Controllers
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
        public IActionResult Index(int pageNumber = 1)
        {
            ClientViewModel viewModel = new ClientViewModel();

            var sessionFilter = HttpContext.Session.Get(Constants.ClientFilter);
            if (sessionFilter != null)
                viewModel.ClientFIlter = Converter.DictionaryToObject<ClientFilter>(sessionFilter);
           
            var sessionSortState = HttpContext.Session.Get(Constants.ClientSort);
            if (sessionSortState != null && sessionSortState.Count > 0)
            {
                ClientSort.State currSortState = (ClientSort.State)Enum.Parse(typeof(ClientSort.State), sessionSortState["sortState"]);
                viewModel.ClientSort = new ClientSort(currSortState);
            }

            HttpContext.Session.Set<int>(Constants.ClientPageNumber,pageNumber);
            SetClients(viewModel, pageNumber);

            return View(viewModel);
        }

        [HttpGet]
        [SetToSession(Constants.ClientSort)]
        public IActionResult Sort(ClientSort.State sortState = ClientSort.State.NoSort)
        {
            ClientViewModel viewModel = new ClientViewModel();
          
            var sessionFilter = HttpContext.Session.Get(Constants.ClientFilter);
            if (sessionFilter != null)
                 viewModel.ClientFIlter = Converter.DictionaryToObject<ClientFilter>(sessionFilter);
            viewModel.ClientSort = new ClientSort(sortState);

            int pageNumber = HttpContext.Session.Get<int>(Constants.ClientPageNumber);
            if (pageNumber < 1) pageNumber = 1;
        

            SetClients(viewModel,pageNumber);

            return View("Index",viewModel);
        }

        [HttpPost]
        [SetToSession(Constants.ClientFilter)]
        public IActionResult Filter(ClientFilter clientFilter)
        {
            ClientViewModel viewModel = new ClientViewModel();

            var sessionSortState = HttpContext.Session.Get(Constants.ClientSort);
            if (sessionSortState != null && sessionSortState.Count > 0)
            {
                ClientSort.State currSortState = (ClientSort.State)Enum.Parse(typeof(ClientSort.State), sessionSortState["sortState"]);
                viewModel.ClientSort = new ClientSort(currSortState);
            }
            viewModel.ClientFIlter = clientFilter;

            int pageNumber = HttpContext.Session.Get<int>(Constants.ClientPageNumber);
            if (pageNumber < 1) pageNumber = 1;

            SetClients(viewModel, pageNumber);

            return View("Index",viewModel);
        }

        // GET: Brands/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = getClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( Client client)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(client);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(client);
        }


        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                if (ClientsExists(client.Id))
                {
                    return View(client);
                }
                _db.Add(client);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }


        // GET: Brands/Details/5
        public IActionResult More(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = getClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }



        // GET: Brands/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = getClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            var client = getClientById(Id);
            _db.Clients.RemoveRange(client);
            var tours = _db.Tours.Where(c => c.ClientId == Id);
            _db.Tours.RemoveRange(tours);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool ClientsExists(int id)
        {
            return _db.Clients.Any(e => e.Id == id);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Client getClientById(int? id)
        {
            return _db.Clients.Where(e => e.Id == id).ToList()[0];
        }


        private void SetClients(ClientViewModel viewModel, int pageNumber)
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

            int pageSize = 10;
            viewModel.PageViewModel = new PageViewModel(clients.Count(), pageNumber, pageSize);
            clients = clients.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            viewModel.Clients = clients;
        }
    }
}
