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
        private readonly IMarkService _markService;
        private readonly IUserService _userService;
        private readonly ISelectionService _selectionService;
        private readonly IParameterService _parameterService;

        public ChiefController(IMarkService markService, IUserService userService, ISelectionService selectionService, IParameterService parameterService)
        {
            _markService = markService;
            _userService = userService;
            _selectionService = selectionService;
            _parameterService = parameterService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> ChooseSelection()
        {
            int currentUserId = Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value);
            User currentUser = await _userService.GetUserById(currentUserId);

            ChooseSelectionViewModel mymodel = new ChooseSelectionViewModel();
            mymodel.Selections = await _selectionService.GetAllDataFromYourDepartment(currentUser.DepartmentId);
            mymodel.Parameters = await _parameterService.GetAllData();
            return View(mymodel);
        }

        public async Task<List<(User user, double mark)>> ConstructUsersList(int selectionId)
        {
            User chief = await _userService.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            List<User> users = await _userService.GetUsersByDepartmentIdNotChief(chief.DepartmentId);

            Selection selection = await _selectionService.GetSelectionById(selectionId);

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
                    total += Math.Round(average * parameters[kv.Key].Coefficient, 2);
                }

                markedUsers.Add((user, total));
            }

            return(markedUsers);
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> SelectionDetails(int selectionId)
        {
            var markedUsers = await ConstructUsersList(selectionId);

            var dictBottomUsers = markedUsers.OrderBy(u => u.mark).Take(3).ToList();
            var dictTopUsers = markedUsers.OrderByDescending(u => u.mark).Take(3).ToList();

            GetAllSelectionsViewModel mymodel = new GetAllSelectionsViewModel();
            mymodel.DictTopUsers = dictTopUsers;
            mymodel.DictBottomUsers = dictBottomUsers;
            return View(mymodel);
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> ExportToExcel(int selectionId)
        {
            Selection selection = await _selectionService.GetSelectionById(selectionId);

            var markedUsers = await ConstructUsersList(selectionId);

            var dictBottomUsers = markedUsers.OrderBy(u => u.mark).Take(3).ToList();
            var dictTopUsers = markedUsers.OrderByDescending(u => u.mark).Take(3).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Top Users";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Surname";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Role";
                foreach (var item in dictTopUsers)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.user.Surname;
                    worksheet.Cell(currentRow, 2).Value = item.user.Name;
                    worksheet.Cell(currentRow, 3).Value = item.user.Role.Name;
                    worksheet.Cell(currentRow, 4).Value = item.mark;
                }
                currentRow += 2;
                worksheet.Cell(currentRow, 1).Value = "Bottom Users";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Surname";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Role";
                foreach (var item in dictBottomUsers)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.user.Surname;
                    worksheet.Cell(currentRow, 2).Value = item.user.Name;
                    worksheet.Cell(currentRow, 3).Value = item.user.Role.Name;
                    worksheet.Cell(currentRow, 4).Value = item.mark;
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
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> GetAllMarks()
        {
            List<Mark> marks = await _markService.GetAllData();
            List<User> assessors = new List<User>();
            foreach (var item in marks)
            {
                assessors.Add(await _userService.GetUserById(item.AssessorId));
            }

            User user = await _userService.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            GetAllMarksViewModel mymodel = new GetAllMarksViewModel();
            mymodel.CurrentUserDepartmentId = user.DepartmentId;
            mymodel.Assessors = assessors;
            mymodel.Marks = marks;
            mymodel.Parameters = await _parameterService.GetAllData();
            mymodel.Users = await _userService.GetAllData();

            return View(mymodel);
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> GetAllActualMarks()
        {
            List<Mark> marks = await _markService.GetAllData();
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

            User user = await _userService.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            GetAllMarksViewModel mymodel = new GetAllMarksViewModel();
            mymodel.CurrentUserDepartmentId = user.DepartmentId;
            mymodel.Marks = actualMarks;
            mymodel.Parameters = await _parameterService.GetAllData();
            mymodel.Users = await _userService.GetAllData();

            return View(mymodel);
        }

        [Authorize(Roles = "Chief")]
        [HttpGet]
        public async Task<IActionResult> AddMark()
        {
            User user = await _userService.GetUserById(Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value));
            AddMarkViewModel mymodel = new AddMarkViewModel();
            mymodel.LeadDepartmentId = user.DepartmentId;
            mymodel.Users = await _userService.GetAllData();
            return View(mymodel);
        }

        [Authorize(Roles = "Chief")]
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

        [Authorize(Roles = "Chief")]
        [HttpPost]
        public async Task<IActionResult> AddMarkAction(int userId, int parameterId, int markValue, string markDescription)
        {
            await _markService.AddMark(userId, parameterId, markValue, markDescription);
            return RedirectToAction("AddMark", "Chief");
        }
    }
}
