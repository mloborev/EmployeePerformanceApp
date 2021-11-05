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
        private readonly ISelectionRepository _selectionRepository;

        private readonly IMarkService _markService;
        public ChiefController(IMarkRepository markRepository, IParameterRepository parameterRepository, IUserRepository userRepository, ISelectionRepository selectionRepository, IMarkService markService)
        {
            _markRepository = markRepository;
            _parameterRepository = parameterRepository;
            _userRepository = userRepository;
            _selectionRepository = selectionRepository;

            _markService = markService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> ChooseSelection()
        {
            ChooseSelectionViewModel mymodel = new ChooseSelectionViewModel();
            mymodel.Selections = await _selectionRepository.GetAllData();
            mymodel.Parameters = await _parameterRepository.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> SelectionDetails(int selectionId)
        {
            User chief = await _userRepository.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            List<User> users = await _userRepository.GetUsersByDepartmentIdNotChief(chief.DepartmentId);

            Selection selection = await _selectionRepository.GetSelectionById(selectionId);

            List<Parameter> selectionParameters = selection.Parameters.ToList();

            object[,] usersTotal = new object[users.Count, 2];
            int counter = 0;
            foreach (var item in users)
            {
                double result = 0;

                foreach (var parameter in selectionParameters)
                {
                    List<Mark> marks = await _markRepository.GetMarkByUserAndDepartmentIds(item.Id, parameter.Id);
                    if(marks.Count == 0)
                    {
                        continue;
                    }

                    int marksSum = 0;
                    foreach(var mark in marks)
                    {
                        marksSum += mark.MarkValue;
                    }
                    result += marksSum / marks.Count * parameter.Coefficient;
                    //result += (x + x1 + x2) / 3 * parameter.Coefficient;      
                }
                usersTotal[counter, 0] = item;
                usersTotal[counter, 1] = result;


                counter++;
            }



            GetAllSelectionsViewModel mymodel = new GetAllSelectionsViewModel();
            return View();
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> GetAllSelections()
        {
            GetAllSelectionsViewModel mymodel = new GetAllSelectionsViewModel();
            mymodel.TopUsers = await _userRepository.GetAllData();
            mymodel.BottomUsers = await _userRepository.GetAllData();
            return View(mymodel);
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
            List<Mark> actualMarks = new List<Mark>();
            TimeSpan diff;
            foreach(Mark item in marks)
            {
                diff = DateTime.Now.Subtract(item.AssesmentDate);
                if(diff.TotalDays < 90)
                {
                    actualMarks.Add(item);
                }
            }

            User user = await _userRepository.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            GetAllMarksViewModel mymodel = new GetAllMarksViewModel();
            mymodel.CurrentUserDepartmentId = user.DepartmentId;
            mymodel.Marks = actualMarks;
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
