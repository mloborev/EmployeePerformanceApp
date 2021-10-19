using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Models.ModelsForViews;
using EmployeePerformanceApp.Repositories;
using EmployeePerformanceApp.Services;
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
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IDepartmentRepository _departmentRepository;

        private readonly IUserService _userService;
        public AdminController(IUserRepository userRepository, IRoleRepository roleRepository, IStatusRepository statusRepository, IDepartmentRepository departmentRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _statusRepository = statusRepository;
            _departmentRepository = departmentRepository;

            _userService = userService;
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
            mymodel.Users = await _userRepository.GetAllData();
            mymodel.Roles = await _roleRepository.GetAllData();
            mymodel.Statuses = await _statusRepository.GetAllData();
            mymodel.Departments = await _departmentRepository.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddUser(string surname, string name, string login, string password, int roleId, int statusId, int departmentId)
        {
            if (!await _userRepository.CheckIsUserExistByLogin(login))
            {
                await _userService.AddUser(surname, name, login, password, roleId, statusId, departmentId);
            }
            else
            {
                ModelState.AddModelError("Error", "User already exists");
            }

            AddUserViewModel mymodel = new AddUserViewModel();
            mymodel.Users = await _userRepository.GetAllData();
            mymodel.Roles = await _roleRepository.GetAllData();
            mymodel.Statuses = await _statusRepository.GetAllData();
            mymodel.Departments = await _departmentRepository.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser()
        {
            AddUserViewModel mymodel = new AddUserViewModel();
            mymodel.Users = await _userRepository.GetAllData();
            mymodel.Roles = await _roleRepository.GetAllData();
            mymodel.Statuses = await _statusRepository.GetAllData();
            mymodel.Departments = await _departmentRepository.GetAllData();

            return View(mymodel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUserAction(int id)
        {
            await _userRepository.DeleteUser(id);
            return RedirectToAction("DeleteUser", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);

            AddUserViewModel mymodel = new AddUserViewModel();
            mymodel.Users = await _userRepository.GetAllData();
            mymodel.Roles = await _roleRepository.GetAllData();
            mymodel.Statuses = await _statusRepository.GetAllData();
            mymodel.Departments = await _departmentRepository.GetAllData();
            return View(mymodel);
        }
    }
}
