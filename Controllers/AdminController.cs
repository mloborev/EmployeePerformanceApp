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
        private readonly IUserService _userService;
        private readonly IParameterService _parameterService;
        private readonly ISelectionService _selectionService;
        private readonly IDepartmentService _departmentService;
        private readonly IRoleService _roleService;
        private readonly IStatusService _statusService;

        public AdminController(IUserService userService, IParameterService parameterService, ISelectionService selectionService, IDepartmentService departmentService, IRoleService roleService, IStatusService statusService)
        {
            _userService = userService;
            _parameterService = parameterService;
            _selectionService = selectionService;
            _departmentService = departmentService;
            _roleService = roleService;
            _statusService = statusService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ChooseDepartment()
        {
            return View(await _departmentService.GetAllData());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddSelection(int departmentId)
        {
            AddSelectionViewModel mymodel = new AddSelectionViewModel();
            mymodel.Departments = await _departmentService.GetAllData();
            mymodel.Selections = await _selectionService.GetAllData();
            mymodel.Parameters = await _parameterService.GetAllData();
            mymodel.DepartmentId = departmentId;
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddSelection(int departmentId, string selectionName, int[] myarray)
        {
            await _selectionService.AddSelection(departmentId, selectionName, myarray);

            AddSelectionViewModel mymodel = new AddSelectionViewModel();
            mymodel.Departments = await _departmentService.GetAllData();
            mymodel.Selections = await _selectionService.GetAllData();
            mymodel.Parameters = await _parameterService.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddParameter()
        {
            AddParameterViewModel mymodel = new AddParameterViewModel();
            mymodel.Departments = await _departmentService.GetAllData();
            mymodel.Parameters = await _parameterService.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddParameter(int departmentId, string name, double coefficient)
        {
            await _parameterService.AddParameter(departmentId, name, coefficient);

            AddParameterViewModel mymodel = new AddParameterViewModel();
            mymodel.Departments = await _departmentService.GetAllData();
            mymodel.Parameters = await _parameterService.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteParameter()
        {
            return View(await _parameterService.GetAllData());
        }

        [HttpGet]
        public async Task<IActionResult> DeleteParameterAction(int id)
        {
            await _parameterService.DeleteParameter(id);
            return RedirectToAction("DeleteParameter", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            AddUserViewModel mymodel = new AddUserViewModel();
            mymodel.Users = await _userService.GetAllData();
            mymodel.Roles = await _roleService.GetAllData();
            mymodel.Statuses = await _statusService.GetAllData();
            mymodel.Departments = await _departmentService.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddUser(string surname, string name, string login, string password, int roleId, int statusId, int departmentId)
        {
            if (!await _userService.CheckIsUserExistByLogin(login))
            {
                await _userService.AddUser(surname, name, login, password, roleId, statusId, departmentId);
            }
            else
            {
                ModelState.AddModelError("Error", "User already exists");
            }

            AddUserViewModel mymodel = new AddUserViewModel();
            mymodel.Users = await _userService.GetAllData();
            mymodel.Roles = await _roleService.GetAllData();
            mymodel.Statuses = await _statusService.GetAllData();
            mymodel.Departments = await _departmentService.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser()
        {
            DeleteUserViewModel mymodel = new DeleteUserViewModel();
            mymodel.Users = await _userService.GetAllData();
            mymodel.Roles = await _roleService.GetAllData();
            mymodel.Statuses = await _statusService.GetAllData();
            mymodel.Departments = await _departmentService.GetAllData();

            return View(mymodel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUserAction(int id)
        {
            //await _userService.DeleteUser(id);
            await _userService.SetWorkingFlag(id);
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
            mymodel.Users = await _userService.GetAllData();
            mymodel.Roles = await _roleService.GetAllData();
            mymodel.Statuses = await _statusService.GetAllData();
            mymodel.Departments = await _departmentService.GetAllData();
            return View(mymodel);
        }
    }
}
