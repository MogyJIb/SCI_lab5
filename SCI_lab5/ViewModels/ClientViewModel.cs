using DbDataLibrary.Models;
using System;
using System.Collections.Generic;
using lab4.Models.Filters;
using lab4.Models.Sorts;


namespace lab4.ViewModels
{
    public class ClientViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public ClientFilter ClientFIlter { get; set; }
        public ClientSort ClientSort { get; set; }

        public ClientViewModel()
        {
            this.ClientFIlter = new ClientFilter();
            this.ClientSort = new ClientSort();
        }
    }
}
