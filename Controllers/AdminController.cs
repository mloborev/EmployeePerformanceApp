using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        public IEnumerable<User> Users { get; set; }
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            AddUserViewModel mymodel = new AddUserViewModel();
            mymodel.Users = await db.Users.Include(u => u.Role).Include(u => u.Status).Include(u => u.Department).ToListAsync();
            mymodel.Roles = await db.Roles.ToListAsync();
            mymodel.Statuses = await db.Statuses.ToListAsync();
            mymodel.Departments = await db.Departments.ToListAsync();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddUser(string surname, string name, string login, string password, int roleId, int statusId, int departmentId)
        {
            User check = await db.Users.Where(x => x.Login == login).FirstOrDefaultAsync();
            if (check == null)
            {
                User user = new User { Surname = surname, Name = name, Login = login, Password = password, RoleId = roleId, StatusId = statusId, DepartmentId = departmentId };
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("Error", "User already exists");
            }

            AddUserViewModel mymodel = new AddUserViewModel();
            mymodel.Users = await db.Users.Include(u => u.Role).Include(u => u.Status).Include(u => u.Department).ToListAsync();
            mymodel.Roles = await db.Roles.ToListAsync();
            mymodel.Statuses = await db.Statuses.ToListAsync();
            mymodel.Departments = await db.Departments.ToListAsync();
            return View(mymodel);
        }
    }
}
