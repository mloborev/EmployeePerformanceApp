using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Controllers
{
    public class AddUserViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }

    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private ApplicationContext db;
        public AdminController(IUserRepository userRepository, ApplicationContext context)
        {
            db = context;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            /*var roles = await db.Roles.ToListAsync();
            var statuses = await db.Statuses.ToListAsync();
            var departments = await db.Departments.ToListAsync();*/
            AddUserViewModel mymodel = new AddUserViewModel();
            mymodel.Roles = await db.Roles.ToListAsync();
            mymodel.Statuses = await db.Statuses.ToListAsync();
            mymodel.Departments = await db.Departments.ToListAsync();
            return View(mymodel);
        }

        /*[HttpPost]
        public IActionResult AddUser()
        {
            
            return View();
        }*/
    }
}
