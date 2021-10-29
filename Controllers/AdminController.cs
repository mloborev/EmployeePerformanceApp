using EmployeePerformanceApp.Context;
using EmployeePerformanceApp.Models;
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
        private readonly IParameterRepository _parameterRepository;
        private readonly ISelectionRepository _selectionRepository;

        private readonly IUserService _userService;
        private readonly IParameterService _parameterService;
        private readonly ISelectionService _selectionService;

        public AdminController(IUserRepository userRepository, IRoleRepository roleRepository, IStatusRepository statusRepository, IDepartmentRepository departmentRepository, IParameterRepository parameterRepository, ISelectionRepository selectionRepository, IUserService userService, IParameterService parameterService, ISelectionService selectionService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _statusRepository = statusRepository;
            _departmentRepository = departmentRepository;
            _parameterRepository = parameterRepository;
            _selectionRepository = selectionRepository;

            _userService = userService;
            _parameterService = parameterService;
            _selectionService = selectionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddSelection()
        {
            AddSelectionViewModel mymodel = new AddSelectionViewModel();
            mymodel.Departments = await _departmentRepository.GetAllData();
            mymodel.Selections = await _selectionRepository.GetAllData();
            mymodel.Parameters = await _parameterRepository.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddSelection(int departmentId, string selectionName, int[] myarray)
        {
            await _selectionService.AddSelection(departmentId, selectionName, myarray);

            AddSelectionViewModel mymodel = new AddSelectionViewModel();
            mymodel.Departments = await _departmentRepository.GetAllData();
            mymodel.Selections = await _selectionRepository.GetAllData();
            mymodel.Parameters = await _parameterRepository.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddParameter()
        {
            AddParameterViewModel mymodel = new AddParameterViewModel();
            mymodel.Departments = await _departmentRepository.GetAllData();
            mymodel.Parameters = await _parameterRepository.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddParameter(int departmentId, string name, double coefficient)
        {
            await _parameterService.AddParameter(departmentId, name, coefficient);

            AddParameterViewModel mymodel = new AddParameterViewModel();
            mymodel.Departments = await _departmentRepository.GetAllData();
            mymodel.Parameters = await _parameterRepository.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteParameter()
        {
            return View(await _parameterRepository.GetAllData());
        }

        [HttpGet]
        public async Task<IActionResult> DeleteParameterAction(int id)
        {
            await _parameterRepository.DeleteParameter(id);
            return RedirectToAction("DeleteParameter", "Admin");
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
            DeleteUserViewModel mymodel = new DeleteUserViewModel();
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
        public async Task<IActionResult> DeleteUser(string lastName, string firstName, string department, string status, string role)
        {
            DeleteUserViewModel mymodel = new DeleteUserViewModel();
            mymodel.LastName = lastName;
            mymodel.FirstName = firstName;
            mymodel.DepartmentName = department;
            mymodel.StatusName = status;
            mymodel.RoleName = role;
            mymodel.Users = await _userRepository.GetAllData();
            mymodel.Roles = await _roleRepository.GetAllData();
            mymodel.Statuses = await _statusRepository.GetAllData();
            mymodel.Departments = await _departmentRepository.GetAllData();
            return View(mymodel);
        }
    }
}
