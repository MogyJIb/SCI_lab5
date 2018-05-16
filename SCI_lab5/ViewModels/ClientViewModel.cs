using DbDataLibrary.Models;
using System;
using System.Collections.Generic;
using SCI_lab5.Models.Filters;
using SCI_lab5.Models.Sorts;


namespace SCI_lab5.ViewModels
{
    public class ClientViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public ClientFilter ClientFIlter { get; set; }
        public ClientSort ClientSort { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public ClientViewModel()
        {
            this.ClientFIlter = new ClientFilter();
            this.ClientSort = new ClientSort();
        }
    }
}
