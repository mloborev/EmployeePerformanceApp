using ClosedXML.Excel;
using EmployeePerformanceApp.Models;
using EmployeePerformanceApp.Repositories;
using EmployeePerformanceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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


            var parameters = selection.Parameters.ToDictionary(p => p.Id, p => p);

            var markedUsers = new List<(User user, double mark)>(users.Count);

            foreach (var user in users)
            {
                var total = 0.0;
                var marks = new Dictionary<int, List<double>>();

                foreach (var mark in user.Marks)
                {
                    if (selection.Parameters.Contains(mark.Parameter))
                    {
                        if (!marks.ContainsKey(mark.ParameterId))
                            marks[mark.ParameterId] = new List<double>();

                        marks[mark.ParameterId].Add(mark.MarkValue);
                    }
                }

                foreach (var kv in marks)
                {
                    var average = kv.Value.Average();
                    total += average * parameters[kv.Key].Coefficient;
                }

                markedUsers.Add((user, total));
            }

            var bottomUsers = markedUsers.OrderBy(u => u.mark).Take(3).Select(t => t.user);
            var topUsers = markedUsers.OrderByDescending(u => u.mark).Take(3).Select(t => t.user);

            GetAllSelectionsViewModel mymodel = new GetAllSelectionsViewModel();
            mymodel.TopUsers = topUsers;
            mymodel.BottomUsers = bottomUsers;
            return View(mymodel);
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> ExportToExcel(int selectionId)
        {
            User chief = await _userRepository.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            List<User> users = await _userRepository.GetUsersByDepartmentIdNotChief(chief.DepartmentId);

            Selection selection = await _selectionRepository.GetSelectionById(selectionId);


            var parameters = selection.Parameters.ToDictionary(p => p.Id, p => p);

            var markedUsers = new List<(User user, double mark)>(users.Count);

            foreach (var user in users)
            {
                var total = 0.0;
                var marks = new Dictionary<int, List<double>>();

                foreach (var mark in user.Marks)
                {
                    if (selection.Parameters.Contains(mark.Parameter))
                    {
                        if (!marks.ContainsKey(mark.ParameterId))
                            marks[mark.ParameterId] = new List<double>();

                        marks[mark.ParameterId].Add(mark.MarkValue);
                    }
                }

                foreach (var kv in marks)
                {
                    var average = kv.Value.Average();
                    total += average * parameters[kv.Key].Coefficient;
                }

                markedUsers.Add((user, total));
            }

            var bottomUsers = markedUsers.OrderBy(u => u.mark).Take(3).Select(t => t.user).ToList();
            var topUsers = markedUsers.OrderByDescending(u => u.mark).Take(3).Select(t => t.user).ToList();


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Top Users";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Surname";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Role";
                foreach (var user in topUsers)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.Surname;
                    worksheet.Cell(currentRow, 2).Value = user.Name;
                    worksheet.Cell(currentRow, 3).Value = user.Role.Name;
                }
                currentRow += 2;
                worksheet.Cell(currentRow, 1).Value = "Bottom Users";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Surname";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Role";
                foreach (var user in bottomUsers)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.Surname;
                    worksheet.Cell(currentRow, 2).Value = user.Name;
                    worksheet.Cell(currentRow, 3).Value = user.Role.Name;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"Selection_{selection.Name}_{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}.xlsx");
                }
            }


            //return RedirectToAction("ChooseSelection", "Chief");
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
