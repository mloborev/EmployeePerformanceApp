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
        private readonly IMarkRepository _markRepository;
        private readonly IParameterRepository _parameterRepository;
        private readonly IUserRepository _userRepository;

        private readonly IMarkService _markService;
        public LeadController(IMarkRepository markRepository, IParameterRepository parameterRepository, IUserRepository userRepository, IMarkService markService)
        {
            _markRepository = markRepository;
            _parameterRepository = parameterRepository;
            _userRepository = userRepository;

            _markService = markService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Lead")]
        [HttpGet]
        public async Task<IActionResult> AddMark()
        {
            //User user = await _userRepository.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            return View(await _userRepository.GetAllData());
        }

        /*[Authorize(Roles = "Lead")]
        [HttpPost]
        public async Task<IActionResult> AddMark(int parameterId, int markValue, string markDescription)
        {
            await _markService.AddMark(parameterId, markValue, markDescription);
            return RedirectToAction("Index", "Lead");
        }*/

        [Authorize(Roles = "Lead")]
        [HttpGet]
        public async Task<IActionResult> AddMarkAction(int userId)
        {
            User currentUser = await _userRepository.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));

            AddMarkViewModel mymodel = new AddMarkViewModel();
            mymodel.LeadDepartmentId = currentUser.DepartmentId;
            mymodel.UserId = userId;
            mymodel.Users = await _userRepository.GetAllData();
            mymodel.Parameters = await _parameterRepository.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Lead")]
        [HttpPost]
        public async Task<IActionResult> AddMarkAction(int userId, int parameterId, int markValue, string markDescription)
        {
            await _markService.AddMark(userId, parameterId, markValue, markDescription);
            return RedirectToAction("AddMark", "Lead");
        }
    }
}
