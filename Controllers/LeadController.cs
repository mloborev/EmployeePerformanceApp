using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using EmployeePerformanceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceApp.Controllers
{
    public class LeadController : Controller
    {
        private readonly IMarkService _markService;
        private readonly IUserService _userService;
        private readonly IParameterService _parameterService;

        public LeadController(IMarkService markService, IUserService userService, IParameterService parameterService)
        {
            _markService = markService;
            _userService = userService;
            _parameterService = parameterService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Lead")]
        [HttpGet]
        public async Task<IActionResult> AddMarkViaTable()
        {
            User user = await _userService.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));

            AddMarkViewModel mymodel = new AddMarkViewModel();
            mymodel.Users = await _userService.GetAllDataForDepartmentForLead(user.DepartmentId);
            mymodel.Parameters = await _parameterService.GetAllDataForDepartment(user.DepartmentId);
            mymodel.LeadDepartmentId = user.DepartmentId;
            return View(mymodel);
        }

        [Authorize(Roles = "Lead")]
        [HttpGet]
        public async Task<IActionResult> AddMark()
        {
            User user = await _userService.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            AddMarkViewModel mymodel = new AddMarkViewModel();
            mymodel.LeadDepartmentId = user.DepartmentId;
            mymodel.Users = await _userService.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Lead")]
        [HttpGet]
        public async Task<IActionResult> AddMarkAction(int userId)
        {
            User currentUser = await _userService.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));

            AddMarkViewModel mymodel = new AddMarkViewModel();
            mymodel.LeadDepartmentId = currentUser.DepartmentId;
            mymodel.UserId = userId;
            mymodel.Users = await _userService.GetAllData();
            mymodel.Parameters = await _parameterService.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Lead")]
        [HttpPost]
        public async Task<IActionResult> AddMarkAction(int userId, int parameterId, int markValue, string markDescription)
        {
            await _markService.AddMark(userId, parameterId, markValue, markDescription);
            return RedirectToAction("AddMark", "Lead");
        }

        [Authorize(Roles = "Lead")]
        [HttpPost]
        public async Task<IActionResult> JSAddMarkAction(string userName, string parameterName, string mark)
        {
            User user = await _userService.GetUserByName(userName);
            Parameter parameter = await _parameterService.GetParameterByName(parameterName);

            await _markService.AddMark(user.Id, parameter.Id, Convert.ToInt32(mark), "null");
            return RedirectToAction("AddMarkViaTable", "Lead");
        }
    }
}
