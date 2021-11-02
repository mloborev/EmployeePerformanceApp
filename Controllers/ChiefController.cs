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
    public class ChiefController : Controller
    {
        private readonly IMarkRepository _markRepository;
        private readonly IParameterRepository _parameterRepository;
        private readonly IUserRepository _userRepository;

        private readonly IMarkService _markService;
        public ChiefController(IMarkRepository markRepository, IParameterRepository parameterRepository, IUserRepository userRepository, IMarkService markService)
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

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> GetAllMarks()
        {
            List<Mark> marks = await _markRepository.GetAllData();
            List<User> assessors = new List<User>();
            foreach (var item in marks)
            {
                assessors.Add(await _userRepository.GetUserById(item.AssessorId));
            }

            User user = await _userRepository.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            GetAllMarksViewModel mymodel = new GetAllMarksViewModel();
            mymodel.CurrentUserDepartmentId = user.DepartmentId;
            mymodel.Assessors = assessors;
            mymodel.Marks = marks;
            mymodel.Parameters = await _parameterRepository.GetAllData();
            mymodel.Users = await _userRepository.GetAllData();

            return View(mymodel);
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> GetAllActualMarks()
        {
            List<Mark> marks = await _markRepository.GetAllData();
            List<User> assessors = new List<User>();
            foreach(var item in marks)
            {
                assessors.Add(await _userRepository.GetUserById(item.AssessorId));
            }

            User user = await _userRepository.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            GetAllMarksViewModel mymodel = new GetAllMarksViewModel();
            mymodel.CurrentUserDepartmentId = user.DepartmentId;
            mymodel.Assessors = assessors;
            mymodel.Marks = marks;
            mymodel.Parameters = await _parameterRepository.GetAllData();
            mymodel.Users = await _userRepository.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> AddMark()
        {
            User user = await _userRepository.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            AddMarkViewModel mymodel = new AddMarkViewModel();
            mymodel.LeadDepartmentId = user.DepartmentId;
            mymodel.Users = await _userRepository.GetAllData();
            return View(mymodel);
        }

        /*[Authorize(Roles = "Chief")]
        [HttpPost]
        public async Task<IActionResult> AddMark(int parameterId, int markValue, string markDescription)
        {
            await _markService.AddMark(parameterId, markValue, markDescription);
            return RedirectToAction("Index", "Chief");
        }*/

        [Authorize(Roles = "Chief")]
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

        [Authorize(Roles = "Chief")]
        [HttpPost]
        public async Task<IActionResult> AddMarkAction(int userId, int parameterId, int markValue, string markDescription)
        {
            await _markService.AddMark(userId, parameterId, markValue, markDescription);
            return RedirectToAction("AddMark", "Chief");
        }
    }
}
