using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SCI_lab5.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
