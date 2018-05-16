using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCI_lab5.Extensions.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SCI_lab5.Controllers
{
    [TypeFilter(typeof(LogFilter))]
    [ExceptionFilter]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
