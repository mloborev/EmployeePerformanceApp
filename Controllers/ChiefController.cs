using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Controllers
{
    public class ChiefController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
